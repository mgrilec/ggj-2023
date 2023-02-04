using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageProjectile : Projectile
{
    public float Damage;
    public override void HitEffect(RaycastHit2D hit)
    {
        if (!hit.rigidbody)
        {
            return;
        }

        var damageable = hit.rigidbody.GetComponentInChildren<IDamageable>();
        damageable.Damage(Damage);
    }
}
