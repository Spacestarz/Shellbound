using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Base_enemy : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    bool inAttackRange;
    public float attackRange = 5;
    base_enemi_attack attack;
    public float attackCooling = 5;
    public bool cooling = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        attack = GetComponentInChildren<base_enemi_attack>();
        //Debug.Log(player);
    }

    // Update is called once per frame
    
    public bool Range(float AttackRange)
    {
        return inAttackRange = Physics.CheckSphere(transform.position, AttackRange, LayerMask.GetMask("Player"));
    }

    public IEnumerator Cool()
    {
        cooling = true;
        yield return new WaitForSeconds(attackCooling);
        if (Range(attackRange))
        {
            attack.Melee();
        }
        cooling = false;
    }
    public void stop()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
    }
    public void start()
    {
        GetComponent<NavMeshAgent>().isStopped = false;
    }
}
