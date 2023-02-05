using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour, IDamageable
{
    public int PlayerIndex;

    [Header("Stats")]
    public float StartingHealth;
    public float Health { get; private set; }
    public float Radius { get { return collider?.radius ?? 0f; } }

    [Header("Movement")]
    public float MoveForce;
    public float MaxSpeed;

    private List<PlayerAbility> Abilities = new List<PlayerAbility>();

    private new Rigidbody2D rigidbody;
    private PlayerAimer aimer;
    private HealthBar healthBar;
    private new CircleCollider2D collider;
    private SpriteRenderer sprite;
    

    private void Awake()
    {
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
        healthBar.Set(Health / StartingHealth);
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis($"horizontal-{PlayerIndex}");
        var vertical = Input.GetAxis($"vertical-{PlayerIndex}");

        var input = new Vector2(horizontal, vertical);
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
                    ability.Fire(transform.position, aimer.Angle);
                }
            }
        }
    }

    public void Damage(float damage)
    {
        Health = Mathf.Max(0, Health - damage);
        if (Health <= 0)
        {
            TreeHealthUI.Instance.GameOver();
        }

        healthBar.Set(Health / StartingHealth);
    }
}
