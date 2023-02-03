using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float Speed;
    public float MaxDistance = 10000;
    private float distance;

    public LayerMask HitLayerMask;
    public LayerMask DestroyLayerMask;

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Speed * Time.fixedDeltaTime, HitLayerMask | DestroyLayerMask);
        if (hit)
        {
            if (HitLayerMask == (HitLayerMask | hit.rigidbody.gameObject.layer))
            {
                HitEffect(hit);
            }

            Destroy(gameObject);
        }

        float offset = Speed * Time.fixedDeltaTime;
        distance += offset;
        transform.position += transform.right * offset;

        if (distance > MaxDistance)
        {
            Destroy(gameObject);
        }
    }

    public abstract void HitEffect(RaycastHit2D hit);
}
