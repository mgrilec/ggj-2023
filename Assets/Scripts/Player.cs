using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerIndex;
    public float MoveForce;
    public float MaxSpeed;

    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
    }
}
