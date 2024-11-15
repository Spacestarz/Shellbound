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
    base_enemi_attack attack;
    public float attackcoling = 5;
    bool cooling = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        attack = GetComponentInChildren<base_enemi_attack>();
        //Debug.Log(player);
    }

    // Update is called once per frame
    void Update()
    {
        //kollar om ett objekt som har en layer definerad i player är inom en svere av diametern som defineras av attackrange.
        if (!range())
        {
            //seger åt agent componenten att gå mot punkten definerad i target.position
            agent.SetDestination(target.position);
        }
        else
        {
            agent.SetDestination(transform.position);
            transform.LookAt(target);
            if (!cooling)
            {
                StartCoroutine(cool());
            }
        }
    }
    bool range()
    {
        return inattackrange = Physics.CheckSphere(transform.position, attackrange, LayerMask.GetMask("Player"));
    }
    IEnumerator cool()
    {
        cooling = true;
        yield return new WaitForSeconds(attackcoling);
        if (range())
        {
            attack.male();
        }
        cooling = false;
    }
}
