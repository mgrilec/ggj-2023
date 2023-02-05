using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public Transform transform { get; }
    public float Radius { get; }
    void Damage(float damage);
    void DamageOverTime(float damage, bool stacks, string stackingTag);
    void Slow(float amount, bool stacks, string stackingTag);
}
