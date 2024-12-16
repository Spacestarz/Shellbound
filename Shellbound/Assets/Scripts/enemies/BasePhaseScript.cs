using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BasePhaseScript : MonoBehaviour
{
    public abstract void phase();
    public bool stunable = false;
    public NavMeshAgent agent;
    public Base_enemy enemy;
    public GameObject player;
    public int i = 1;
    // Start is called before the first frame update
    public void resetpositon()
    {
        agent.SetDestination(transform.position);
    }
    public void Move()
    {
        agent.SetDestination(player.transform.position);
    }
}
