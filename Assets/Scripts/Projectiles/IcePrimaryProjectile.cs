using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePrimaryProjectile : Projectile
{
    public float Damage;
    public float SlowAmount;

    public override void HitEffect(RaycastHit2D hit)
    {
        if (!hit.rigidbody)
        {
            return;
        }

        var damageable = hit.rigidbody.GetComponent<IDamageable>();
        damageable.Damage(Damage);

        if (SlowAmount > 0f)
        {
            damageable.Slow(SlowAmount, Stacks, StackingTag);
        }
    }
}
