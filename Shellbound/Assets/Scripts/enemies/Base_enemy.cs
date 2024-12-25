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
    public bool atta = true;
    public bool volnereble = false;
    public float volnerebleTime = 5;
    public int phase = 1;
   

    AudioSource sorce;
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
        GetComponent<NavMeshAgent>().isStopped = false;
    }
    public void attacking()
    {
        StartCoroutine(canattack());
    }
    public IEnumerator weekTimer()
    {
        try { GetComponentInChildren<BaseAnimator>().anim.SetTrigger("Vulnerable"); }
        catch { }
        agent.SetDestination(transform.position);
        volnereble = true;
        target.GetComponent<PlayerController>().harpoontime = true;
        yield return new WaitForSeconds(volnerebleTime);
        volnereble = false;
        target.GetComponent<PlayerController>().harpoontime = false;
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }
    public void wekend()
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dart") && !volnereble)
        {
            PlayBonk();
        }
    }

    public void PlayBonk()
    {
        sorce.PlayOneShot(bonk, 0.15f);
    }
}
