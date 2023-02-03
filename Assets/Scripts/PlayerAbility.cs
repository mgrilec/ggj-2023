using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility : MonoBehaviour
{
    public Projectile ProjectilePrefab;

    public bool CanFire()
    {
        return true;
    }

    public void Fire(Vector3 origin, float angle)
    {
        if (!CanFire())
        {
            return;
        }

        var instance = Instantiate(ProjectilePrefab.gameObject);
        instance.transform.position = origin;
        instance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}


