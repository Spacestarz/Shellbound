using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Base_enemy : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    bool inAttackRange;
    //public float attackRange = 5;
    public Boss1_attacks attack;
    //public float attackCooling = 5;
    public bool cooling = false;
    public bool atta = true;
    public bool volnereble = false;
    public float volnerebleTime = 5;
    public int phase = 1;

    // Start is called before the first frame update
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        attack = GetComponentInChildren<Boss1_attacks>();
        //Debug.Log(player);
    }


    public bool Range(float AttackRange)
    {
        return inAttackRange = Physics.CheckSphere(transform.position, AttackRange, LayerMask.GetMask("Player"));
    }

    public IEnumerator Cool(float attackCooling, float attackRange)
    {
        //Debug.Log("run");
        cooling = true;
        yield return new WaitForSeconds(attackCooling);
        if (Range(attackRange) && this.enabled == true)
        {
            attack.Melee(attackRange);
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
    public void attacking()
    {
        atta = true;
    }
    public IEnumerator weekTimer()
    {
        Debug.Log("week");
        GetComponentInChildren<MantisAnimator>().anim.SetTrigger("Vulnerable");
        agent.SetDestination(transform.position);
        volnereble = true;
        yield return new WaitForSeconds(volnerebleTime);
        volnereble = false;
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }
    public void wekend()
    {
        StartCoroutine(weekTimer());
    }

    public void StopWeakTimer()
    {
        StopCoroutine(weekTimer());
    }
}
