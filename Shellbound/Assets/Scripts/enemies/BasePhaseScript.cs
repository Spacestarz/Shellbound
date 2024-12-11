using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BasePhaseScript : MonoBehaviour
{
    public abstract void phase();
    public NavMeshAgent agent;
    public Base_enemy enemy;
    public int i = 1;
    // Start is called before the first frame update
    public void resetpositon()
    {
        agent.SetDestination(transform.position);
    }
}
