using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour, IDamageable
{
    public int PlayerIndex;

    [Header("Movement")]
    public float MoveForce;
    public float MaxSpeed;

    private List<PlayerAbility> Abilities = new List<PlayerAbility>();

    private new Rigidbody2D rigidbody;
    private PlayerAimer aimer;

    public void Damage(float damage)
    {
        Debug.Log("Damage " + damage);
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        aimer = GetComponentInChildren<PlayerAimer>();
        Abilities.AddRange(GetComponentsInChildren<PlayerAbility>());
    }

    // Start is called before the first frame update
    void Start()
    {
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
            foreach (var ability in Abilities)
            {
                if (Input.GetButton($"{ability.Key}-{PlayerIndex}") && ability.CanFire())
                {
                    ability.Fire(transform.position, aimer.Angle);
                }
            }
        }
    }
}
