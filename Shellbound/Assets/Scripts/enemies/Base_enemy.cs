using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Base_enemy : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    bool inAttackRange;
    public float attackRange = 5;
    base_enemi_attack attack;
    public float attackCooling = 5;
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
        if (!Range())
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
                StartCoroutine(Cool());
            }
        }
    }
    bool Range()
    {
        return inAttackRange = Physics.CheckSphere(transform.position, attackRange, LayerMask.GetMask("Player"));
    }

    IEnumerator Cool()
    {
        cooling = true;
        yield return new WaitForSeconds(attackCooling);
        if (Range())
        {
            attack.Melee();
        }
        cooling = false;
    }
}
