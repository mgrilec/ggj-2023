using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility: MonoBehaviour
{
    public string Key;
    public Projectile ProjectilePrefab;
    public float Cooldown;

    private float lastFireTime;
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public bool CanFire()
    {
        return Time.time - lastFireTime > Cooldown;
    }

    public void Fire(Vector3 origin, float angle)
    {
        if (!CanFire())
        {
            return;
        }

        lastFireTime = Time.time;

        var instance = GameObject.Instantiate(ProjectilePrefab.gameObject);
        instance.transform.position = origin;
        instance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}


