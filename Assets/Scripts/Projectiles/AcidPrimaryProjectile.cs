using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPrimaryProjectile : Projectile
{
    public float Damage;
    public float SplashDamage;
    public float SplashRadius;

    public override void HitEffect(RaycastHit2D hit)
    {
        if (hit.rigidbody)
        {
            var damageable = hit.rigidbody.GetComponent<IDamageable>();
            damageable.Damage(Damage);
        }

        var colliders = Physics2D.OverlapCircleAll(hit.point, SplashRadius, HitLayerMask);
        foreach (var collider in colliders)
        {
            if (collider.attachedRigidbody)
            {
                var damageable = collider.attachedRigidbody.GetComponent<IDamageable>();
                damageable.Damage(SplashDamage);
            }
        }
    }
}
