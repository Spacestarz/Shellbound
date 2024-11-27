using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_AI : Base_enemy
{
    //statemachine
    public GameObject clae;
    public GameObject wave1;
    public GameObject wave2;
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
    int i = 1;

    private void Awake()
    {

    }
    void Update()
    {
        //kollar om ett objekt som har en layer definerad i player �r inom en svere av diametern som defineras av attackrange.
        if (Range(startpunchrange))
        {
            resetpositon();
            //transform.LookAt(target);
            if (!cooling)
            {
                StartCoroutine(Cool(punchspeed, punchrange));
            }
        }
        else if (Range(startelastickrange) && atta && !volnereble && phase == 2)
        {
            atta = false;
            if (i % 4 == 0)
            {
                attack.Elastick(elastickrange, elastickspeed, elastickreturnspeed);
            }
            else
            {
                StartCoroutine(attack.shockwave(shockwavespeed, shockwavezise, shockwaverange));

            }
            i++;
            resetpositon();
        }
        else if (Range(startelastickrange) && atta && !volnereble && phase == 3)
        {
            atta = false;
            if (i % 4 == 0)
            {
                attack.Elastick(elastickrange, elastickspeed, elastickreturnspeed);
            }
            else
            {
                StartCoroutine(attack.shockwave(shockwavespeed, shockwavezise, shockwaverange));
                StartCoroutine(attack.shockwave(shockwavespeed, shockwavezise, shockwaverange));
            }
            i++;
            resetpositon();
        }
        else if (Range(startelastickrange) && atta && !volnereble)
        {
            attack.Elastick(elastickrange, elastickspeed, elastickreturnspeed);
            atta = false;
            resetpositon();
        }
        else if (atta && !volnereble)
        {
            //seger �t agent componenten att g� mot punkten definerad i target.position
            agent.SetDestination(target.position);
        }
        else
        {
            resetpositon();
        }

    }
    public void resetpositon()
    {
        agent.SetDestination(transform.position);
    }
    
}
