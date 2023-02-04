using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IDamageable
{
    public static Tree Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Damage(float damage)
    {

    }
}
