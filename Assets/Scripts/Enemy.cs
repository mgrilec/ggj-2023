using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AttackPreference
{
    Player,
    Tree,
    Whateva
}

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Move")]
    public bool RotatesWhileMoving;
    public float RotatesWhileMovingOffset;

    [Header("Stats")]
    public float StartingHealth = 20f;
    public float Health { get; private set; }
    public float Radius { get { return collider?.radius ?? 0f; } }

    [Header("Attack")]
    public AttackPreference AttackPreference;
    public float AttackDamage;
    public float AttackRange;
    public float AttackCooldown;
    private float lastAttackTime;

    [Header("Corpses")]
    public List<GameObject> CorpsePrefabs = new List<GameObject>();

    NavMeshAgent agent;

    Player[] players;
    List<IDamageable> visibleTargets = new List<IDamageable>();
    List<IDamageable> visibleTargetsWithinRange = new List<IDamageable>();
    HealthBar healthBar;
    new CircleCollider2D collider;
    private SpriteRenderer sprite;

    private void Awake()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        players = FindObjectsOfType<Player>();
        healthBar = GetComponentInChildren<HealthBar>();
        collider = GetComponentInChildren<CircleCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = StartingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (RotatesWhileMoving)
        {
            Vector3 velocityNormalized = agent.velocity.normalized;
            float angle = Mathf.Atan2(velocityNormalized.y, velocityNormalized.x) * Mathf.Rad2Deg;
            sprite.transform.rotation = Quaternion.AngleAxis(angle + RotatesWhileMovingOffset, Vector3.forward);
        }

        // find closest player
        Player closestPlayer = players[0];
        float closestPlayerDistanceSqr = float.MaxValue;
        foreach (var player in players)
        {
            float distanceSqr = (transform.position - player.transform.position).sqrMagnitude;
            if (distanceSqr < closestPlayerDistanceSqr)
            {
                closestPlayerDistanceSqr = distanceSqr;
                closestPlayer = player;
            }
        }

        visibleTargets.Clear();
        if (AttackPreference == AttackPreference.Player || AttackPreference == AttackPreference.Whateva)
        {
            foreach (var player in players)
            {
                NavMeshHit hit;
                if (!agent.Raycast(player.transform.position, out hit))
                {
                    visibleTargets.Add(player);
                }
            }
        }
        
        if (AttackPreference == AttackPreference.Tree || AttackPreference == AttackPreference.Whateva)
        {
            NavMeshHit treeHit;
            if (Tree.Instance && !agent.Raycast(Tree.Instance.transform.position, out treeHit))
            {
                visibleTargets.Add(Tree.Instance);
            }
        }
        

        // sort targets
        visibleTargets.Sort((a, b) =>
        {
            if (AttackPreference == AttackPreference.Player)
            {
                if (a is Player && b is Tree)
                {
                    return -1;
                }
                else if (a is Tree && b is Player)
                {
                    return 1;
                }
            }
            else if (AttackPreference == AttackPreference.Tree)
            {
                if (a is Player && b is Tree)
                {
                    return 1;
                }
                else if (a is Tree && b is Player)
                {
                    return -1;
                }
            }

            float aDistance = (transform.position - a.transform.position).sqrMagnitude;
            float bDistance = (transform.position - b.transform.position).sqrMagnitude;
            return aDistance.CompareTo(bDistance);
        });

        visibleTargetsWithinRange.Clear();
        visibleTargetsWithinRange.AddRange(visibleTargets.FindAll(target =>
        {
            float distanceSqr = (transform.position - target.transform.position).sqrMagnitude;
            return distanceSqr < (AttackRange + this.Radius + target.Radius) * (AttackRange + this.Radius + target.Radius);
        }));

        if (visibleTargetsWithinRange.Count > 0)
        {
            var target = visibleTargetsWithinRange[0];
            Attack(target);
            agent.isStopped = true;
        }
        else
        {
            if (AttackPreference == AttackPreference.Tree && Tree.Instance)
            {
                agent.isStopped = false;
                agent.SetDestination(Tree.Instance.transform.position);
            }
            else if (visibleTargets.Count > 0)
            {
                agent.isStopped = false;
                agent.SetDestination(visibleTargets[0].transform.position);
            }
            else if (Tree.Instance)
            {
                agent.isStopped = false;
                agent.SetDestination(Tree.Instance.transform.position);
            }
        }
    }

    public bool CanAttack()
    {
        return Time.time - lastAttackTime > AttackCooldown;
    }

    public void Attack(IDamageable target)
    {
        if (!CanAttack())
        {
            return;
        }

        target.Damage(AttackDamage);
        lastAttackTime = Time.time;
    }

    public void Damage(float damage)
    {
        Health = Mathf.Max(0f, Health - damage);
        healthBar.Set(Health / StartingHealth);

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (CorpsePrefabs.Count > 0)
        {
            GameObject corpsePrefab = CorpsePrefabs[Random.Range(0, CorpsePrefabs.Count)];
            var instance = Instantiate(corpsePrefab);
            instance.transform.position = transform.position;
        }
    }
}
