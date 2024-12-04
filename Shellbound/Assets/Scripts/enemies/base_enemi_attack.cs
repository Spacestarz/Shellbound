using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class base_enemi_attack : MonoBehaviour
{
    public abstract void phase();
    public Crowd_attacks crowd;
    public Base_enemy enemy;
    public Boss1_attacks attack;
    public NavMeshAgent agent;
    public int wavemount = 1;
    public int i = 1;
    [Header("punch")]
    public float startpunchrange = 7;
    public float punchrange = 5;
    public float punchspeed = 4;
    [Header("shockwave")]
    public float startshockwaverange = 8;
    public float shockwaverange = 15;
    public float shockwavespeed = 3;
    public float shockwavezise = 3;
    [Header("elastick")]
    public float startelastickrange = 12;
    public float elastickrange = 12;
    public float elastickspeed = 4;
    public float elastickreturnspeed = 10;
    private void Start()
    {
        enemy = GetComponentInParent<Base_enemy>();
        //attak = GetComponentInParent<Boss1_attacks>();
        agent = GetComponentInParent<NavMeshAgent>();
    }
    public void resetpositon()
    {
        agent.SetDestination(transform.position);
    }
 
    public IEnumerator cooldown(float t)
    {
        
        //yield return new WaitForSeconds(t);
        enemy.atta = true;
        attack.parent.start();
        attack.still = false;
        yield return null;
    }
    public IEnumerator dublewave(int amount)
    {
        yield return new WaitForSeconds(0.7f);
        for (int j = 0; j < amount; j++)
        {
            attack.shockwave(shockwavespeed, shockwavezise, shockwaverange);
            yield return new WaitForSeconds(0.7f);

        }
        //attack.shockwave(shockwavespeed, shockwavezise, shockwaverange);
        StartCoroutine(cooldown(shockwavespeed));
    }
}
