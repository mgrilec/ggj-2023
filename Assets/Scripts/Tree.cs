using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IDamageable
{
    public static Tree Instance;

    public float StartingHealth;
    public float Health { get; private set; }

    public float Radius 
    {
        get { return collider?.radius ?? 0f; }
    }

    private new CircleCollider2D collider;

    private void Awake()
    {
        Instance = this;
        collider = GetComponentInChildren<CircleCollider2D>();
    }

    private void Start()
    {
        Health = StartingHealth;
    }

    public void Damage(float damage)
    {
        Health = Mathf.Max(0, Health - damage);
        if (Health <= 0)
        {
            // game over
        }
    }
}
