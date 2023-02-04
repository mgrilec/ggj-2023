using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IDamageable
{
    public static Tree Instance;

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

    public void Damage(float damage)
    {

    }
}
