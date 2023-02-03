using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;

    Player[] players;

    private void Awake()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        players = FindObjectsOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
}
