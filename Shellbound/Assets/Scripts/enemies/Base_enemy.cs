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
    public BossAttacksCommon attack;
    //public float attackCooling = 5;
    public bool atta = true;    //unclear variable name
    public bool volnereble = false;
    public float volnerebleTime = 5;
    public int phase = 1;

    public weekpoint weakPoint;

    AudioSource sorce; //source*
    [SerializeField] AudioClip bonk;

    // Start is called before the first frame update
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerSlice.instance.transform;//GameObject.Find("Player").transform;
        attack = GetComponentInChildren<BossAttacksCommon>();
        sorce = GetComponent<AudioSource>();
        //Debug.Log(player);
    }


    public bool Range(float AttackRange)
    {
        return inAttackRange = Physics.CheckSphere(transform.position, AttackRange, LayerMask.GetMask("Player"));
    }

    
    public void stop()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
    }
    public void start()
    {
        try
        {
            GetComponent<NavMeshAgent>().isStopped = false;
        }
        catch { }
    }
    public void attacking()
    {
        StartCoroutine(canattack());
    }
    public IEnumerator weekTimer() //weak*
    {
        try { GetComponentInChildren<BaseAnimator>().anim.SetTrigger("Vulnerable"); }
        catch { }
        agent.SetDestination(transform.position);
        volnereble = true;
        target.GetComponent<PlayerController>().harpoonTime = true;
        yield return new WaitForSeconds(volnerebleTime);
        volnereble = false;
        target.GetComponent<PlayerController>().harpoonTime = false;
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }
    public void wekend() //weakened*
    {
        GetComponentInChildren<SoundcueHandler>().StopPlaying();
        StopWeakTimer(); //To really really make sure only one of these coroutines is running at a time.
        StartCoroutine(weekTimer());
    }

    public void StopWeakTimer()
    {
        StopCoroutine(weekTimer());
    }
    public IEnumerator canattack()
    {
        yield return new WaitForSeconds(1);
        atta = true;
    }

    public void PlayBonk()
    {
        GetComponentInChildren<BaseAnimator>().anim.SetTrigger("Block");
        sorce.PlayOneShot(bonk, 0.1f);
    }
}
