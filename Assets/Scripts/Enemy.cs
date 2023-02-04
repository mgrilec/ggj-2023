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
    [Header("Stats")]
    public float StartingHealth = 20f;
    public float Health { get; private set; }

    [Header("Attack")]
    public AttackPreference AttackPreference;
    public float AttackRange;
    public float AttackCooldown;
    private float lastAttackTime;

    NavMeshAgent agent;

    Player[] players;
    List<IDamageable> visibleTargets = new List<IDamageable>();
    List<IDamageable> visibleTargetsWithinRange = new List<IDamageable>();
    HealthBar healthBar;

    private void Awake()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        players = FindObjectsOfType<Player>();
        healthBar = GetComponentInChildren<HealthBar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = StartingHealth;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (agent.SetDestination(closestPlayer.transform.position))
        {
            
        }

        visibleTargets.Clear();
        foreach (var player in players)
        {
            NavMeshHit hit;
            if (!agent.Raycast(player.transform.position, out hit))
            {
                visibleTargets.Add(player);
            }
        }

        NavMeshHit treeHit;
        if (Tree.Instance && !agent.Raycast(Tree.Instance.transform.position, out treeHit))
        {
            visibleTargets.Add(Tree.Instance);
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
            return distanceSqr < AttackRange * AttackRange;
        }));
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
}
