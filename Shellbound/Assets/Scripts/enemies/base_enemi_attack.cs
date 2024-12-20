using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class base_enemi_attack : BasePhaseScript
{
    public UrchinSpawner UrchinSpawnerScript;

    //public abstract void phase();
    public Crowd_attacks crowd;
    
    public Boss1_attacks attack;
    
    public int wavemount = 1;
    
    [Header("anim bools")]
    public bool ElastickAnim = false;
    public bool WaveAnim = false;
    [Header("punch")]
    public float startpunchrange = 7;
    public float punchrange = 5;
    public float punchspeed = 4;
    [Header("shockwave")]
    public float startshockwaverange = 8;
    public float shockwaverange = 15;
    public float shockwavespeed = 3;
    public float shockwavezise = 3;
    public float shockwavetimer = 0.7f;
    [Header("elastick")]
    public float startelastickrange = 12;
    public float elastickrange = 12;
    public float elastickspeed = 4;
    public float elastickreturnspeed = 10;
    public float elastickdelai = 2;
    private void Awake()
    {
        UrchinSpawnerScript = FindObjectOfType<UrchinSpawner>();
        enemy = GetComponentInParent<Base_enemy>();
        //attak = GetComponentInParent<Boss1_attacks>();
        agent = GetComponentInParent<NavMeshAgent>();
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
        if(amount == 1)
        {
            enemy.GetComponentInChildren<MantisAnimator>().anim.SetTrigger("Shockwave");
        }
        else if(amount > 1)
        {
            amount = 2;
            enemy.GetComponentInChildren<MantisAnimator>().anim.SetTrigger("Double Shockwave");
        }
        WaveAnim = true;
        yield return new WaitForSeconds(2.9f);
        stunable = true;
        if (!enemy.volnereble)
        {

            for (int j = 0; j < amount; j++)
            {
                if (!enemy.volnereble)
                { 
                   
                    WaveAnim = false;
                    attack.shockwave(shockwavespeed, shockwavezise, shockwaverange);
                    if (j == 0)
                    {
                       //spawning urchins
                       UrchinSpawnerScript.WhichPhaseForUrchin();
                    }
                    yield return new WaitForSeconds(1.0f);
                    WaveAnim = true;
                }
            }
        }
        WaveAnim = false;
        stunable = false;
        //attack.shockwave(shockwavespeed, shockwavezise, shockwaverange);
        StartCoroutine(cooldown(shockwavespeed));
    }
    public IEnumerator elestickdelay(float time)
    {
        ElastickAnim = true;
        enemy.atta = false;
        stunable = true;
        enemy.GetComponentInChildren<MantisAnimator>().anim.SetTrigger("Punch 0");
        yield return new WaitForSeconds(time);
        ElastickAnim = false;
        if (!enemy.volnereble && !PlayerSlice.SliceMode())
        {
            attack.Elastick(elastickrange, elastickspeed, elastickreturnspeed);
        }
        else
        {
            enemy.atta = true;
            stunable = false;
        }
    }
}
