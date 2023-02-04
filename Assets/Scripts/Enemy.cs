using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public float StartingHealth = 20f;
    public float Health { get; private set; }

    NavMeshAgent agent;

    Player[] players;
    HealthBar healthBar;

    private void Awake()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        players = FindObjectsOfType<Player>();
        healthBar = GetComponentInChildren<HealthBar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = StartingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // find closest player
        Player closestPlayer = players[0];
        float closestPlayerDistanceSqr = float.MaxValue;
        foreach (var player in players)
        {
            float distanceSqr = (transform.position - player.transform.position).sqrMagnitude;
            if (distanceSqr < closestPlayerDistanceSqr)
            {
                closestPlayerDistanceSqr = distanceSqr;
                closestPlayer = player;
            }
        }

        if (agent.SetDestination(closestPlayer.transform.position))
        {
            
        }
    }

    public void Damage(float damage)
    {
        Health = Mathf.Max(0f, Health - damage);
        healthBar.Set(Health / StartingHealth);

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
