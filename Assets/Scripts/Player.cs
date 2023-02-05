using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour, IDamageable
{
    public int PlayerIndex;

    [Header("Stats")]
    public float StartingHealth;
    public float MaxHealth;
    public float Health { get; private set; }
    public float Radius { get { return collider?.radius ?? 0f; } }
    public float Spread = 1f;

    [Header("Movement")]
    public float MoveForce;
    public float MaxSpeed;

    [Header("Animation")]
    public string MoveStateName;
    public string IdleStateName;

    private List<PlayerAbility> Abilities = new List<PlayerAbility>();

    private Animator animator;
    private new Rigidbody2D rigidbody;
    private PlayerAimer aimer;
    private HealthBar healthBar;
    private new CircleCollider2D collider;
    private SpriteRenderer sprite;
    

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        aimer = GetComponentInChildren<PlayerAimer>();
        collider = GetComponentInChildren<CircleCollider2D>();
        Abilities.AddRange(GetComponentsInChildren<PlayerAbility>());
        healthBar = GetComponentInChildren<HealthBar>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = StartingHealth;
        healthBar.Set(Health / MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis($"horizontal-{PlayerIndex}");
        var vertical = Input.GetAxis($"vertical-{PlayerIndex}");
        var input = new Vector2(horizontal, vertical);
        if (input.sqrMagnitude > 0.1f)
        {
            animator.Play(MoveStateName);
        }
        else
        {
            animator.Play(IdleStateName);
        }

        rigidbody.AddForce(input * MoveForce);

        var velocitySqr = rigidbody.velocity.sqrMagnitude;
        if (velocitySqr > MaxSpeed * MaxSpeed)
        {
            var velocityNormalized = rigidbody.velocity.normalized;
            rigidbody.velocity = new Vector2(velocityNormalized.x, velocityNormalized.y) * MaxSpeed;
        }

        if (aimer.Aiming)
        {
            sprite.transform.rotation = Quaternion.AngleAxis(aimer.Angle + 90f, Vector3.forward);

            foreach (var ability in Abilities)
            {
                if (Input.GetButton($"{ability.Key}-{PlayerIndex}") && ability.CanFire())
                {
                    ability.Fire(transform.position, aimer.Angle + Random.Range(-Spread, Spread));
                }
            }
        }
    }

    public void Damage(float damage)
    {
        if (Tree.Instance.Orbs >= 3)
        {
            return;
        }

        Health = Mathf.Max(0, Health - damage);
        if (Health <= 0)
        {
            TreeHealthUI.Instance.GameOver();
        }

        healthBar.Set(Health / MaxHealth);
    }

    public void DamageOverTime(float damage, bool stacks, string stackingTag)
    {

    }

    public void Slow(float amount, bool stacks, string stackingTag)
    {

    }

    public void Heal(float amount)
    {
        Health = Mathf.Min(Health + MaxHealth * amount, MaxHealth);
        healthBar.Set(Health / MaxHealth);
    }
}
