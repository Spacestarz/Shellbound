using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Base_enemy : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    bool inattackrange;
    public float attackrange = 5;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        //Debug.Log(player);
    }

    // Update is called once per frame
    void Update()
    {
        //kollar om ett objekt som har en layer definerad i player är inom en svere av diametern som defineras av attackrange.
        inattackrange = Physics.CheckSphere(transform.position, attackrange, LayerMask.GetMask("Player"));
        if (!inattackrange)
        {
            //seger åt agent componenten att gå mot punkten definerad i target.position
            agent.SetDestination(target.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }
}
