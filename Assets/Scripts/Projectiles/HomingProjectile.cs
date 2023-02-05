using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public IDamageable Target;
    public float Damage;
    public float Speed = 10f;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);

        if ((transform.position - Target.transform.position).sqrMagnitude < 1f)
        {
            Target.Damage(Damage);
            Destroy(gameObject);
        }
    }
}
