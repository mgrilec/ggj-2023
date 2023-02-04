using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public Transform transform { get; }
    void Damage(float damage);
}
