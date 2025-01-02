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
    public bool ElastickAnim = false; //elasticAnim*
    public bool WaveAnim = false; //waveAnim*
    [Header("punch")]
    public float startpunchrange = 7; //startPunchRange*
    public float punchrange = 5;    //etc.
    public float punchspeed = 4;
    [Header("shockwave")]
    public float startshockwaverange = 8;
    public float shockwaverange = 15;
    public float shockwavespeed = 3;
    public float shockwavezise = 3; //shockwaveSize*
    public float shockwavetimer = 0.7f;
    [Header("elastick")] //elastic*
    public float startelastickrange = 12;
    public float elastickrange = 12;
    public float elastickspeed = 4;
    public float elastickreturnspeed = 10;
    public float elastickdelai = 1.667f; //elasticDelay*

    public AudioClip cueFist;
    public AudioClip cueWave;

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
    } //Line break
    public IEnumerator dublewave(int amount) //DoubleWave*
    {
        float firstWaveDelay = 2.9f;
        float secondWaveDelay = 1.0f;
        stunable = true;    //Line break
        if(amount == 1)
        {
            SoundcueHandler.PlayWaveCue();
            StartCoroutine(enemy.weakPoint.SingleShockwave());
            enemy.GetComponentInChildren<MantisAnimator>().anim.SetTrigger("Shockwave");
        }
        else if(amount > 1)
        {
            SoundcueHandler.PlayDoubleWaveCue();
            StartCoroutine(enemy.weakPoint.DoubleShockwave());
            amount = 2;
            enemy.GetComponentInChildren<MantisAnimator>().anim.SetTrigger("Double Shockwave");
            firstWaveDelay = 2.0f;
            secondWaveDelay = 0.85f;
        } //Line break
        WaveAnim = true;
        yield return new WaitForSeconds(firstWaveDelay);
        enemy.stop();
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
                    yield return new WaitForSeconds(secondWaveDelay);
                    WaveAnim = true;
                }
            }
        } //Line break
        WaveAnim = false;
        stunable = false;
        //attack.shockwave(shockwavespeed, shockwavezise, shockwaverange);
        enemy.start();
        StartCoroutine(cooldown(shockwavespeed));
    } //Line break
    public IEnumerator elestickdelay(float time)
    {
        StartCoroutine(enemy.weakPoint.Fist());

        ElastickAnim = true;
        SoundcueHandler.PlayFistCue();
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
