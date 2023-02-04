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

    public GameObject ExplosionPrefab;

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Speed * Time.fixedDeltaTime, HitLayerMask | DestroyLayerMask);
        if (hit)
        {
            if ((HitLayerMask & 1 << hit.collider.gameObject.layer) != 0)
            {
                HitEffect(hit);
            }

            transform.position = hit.point;
            Kill();
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

    public void Kill()
    {
        if (ExplosionPrefab)
        {
            var instance = Instantiate(ExplosionPrefab);
            instance.transform.position = transform.position;
        }

        Destroy(gameObject);
    }
}
