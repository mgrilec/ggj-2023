using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageProjectile : Projectile
{
    public float Damage;
    public override void HitEffect(RaycastHit2D hit)
    {
        var damageable = hit.collider.GetComponentInChildren<IDamageable>();
        damageable.Damage(Damage);
    }
}
