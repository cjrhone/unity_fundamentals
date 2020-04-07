using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCControl : MonoBehaviour
{
    public float patrolTime = 10f;
    public float aggroRange = 10f;
    public Transform[] waypoints; // collection of transform data

    private int index; // indicates what index we're on in waypoints
    private float speed, agentSpeed; // detect current speed of agent
    private Transform player; // where the player is going to be
    
    // private Animator anim; 
    private NavMeshAgent agent;

    private void Awake()
    {
        // anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if(agent != null)
        {
            agentSpeed = agent.speed;
        }

        player = GameObject.FindGameObjectWithTag("Player").transform; // find player's position
        index = Random.Range(0, waypoints.Length); // random point in array

        InvokeRepeating("Tick", 0, 0.5f);

        if(waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", 0, patrolTime);
        }

        
    }


    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1; // if index == waypoints.Length -1 ( -1 prevents error )
        // FALSE sets index to 0
        // TRUE increments index + 1
    }

    void Tick()
    {
        agent.destination = waypoints[index].position;
        agent.speed = agentSpeed / 2;

        if(player != null && Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            agent.destination = player.position;
            agent.speed = agentSpeed;
        }
    }
}
