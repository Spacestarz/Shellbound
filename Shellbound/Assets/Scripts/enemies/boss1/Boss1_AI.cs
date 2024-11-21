using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_AI : Base_enemy
{
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
    void Update()
    {
        //kollar om ett objekt som har en layer definerad i player är inom en svere av diametern som defineras av attackrange.
        if (Range(startpunchrange))
        {
            agent.SetDestination(transform.position);
            //transform.LookAt(target);
            if (!cooling)
            {
                StartCoroutine(Cool(punchspeed, punchrange));
            }
        }
        else if (Range(startshockwaverange) && atta)
        {
            agent.SetDestination(transform.position);
            atta = false;
            StartCoroutine(attack.shockwave(shockwavespeed, shockwavezise, shockwaverange));
        }
        else if (Range(startelastickrange) && atta)
        {
            agent.SetDestination(transform.position);
            attack.Elastick(elastickrange, elastickspeed, elastickreturnspeed);
            atta = false;
        }
        else
        {
            //seger åt agent componenten att gå mot punkten definerad i target.position
            agent.SetDestination(target.position);
        }

    }
    
}
