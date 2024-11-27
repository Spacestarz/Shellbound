using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrimp_phase_3 : base_enemi_attack
{
    public override void phase()
    {
        if (enemy.Range(startpunchrange))
        {
            resetpositon();
            //transform.LookAt(target);
            if (!enemy.cooling)
            {
                StartCoroutine(enemy.Cool(punchspeed, punchrange));
            }
        }
        else if (enemy.Range(startelastickrange) && enemy.atta && !enemy.volnereble)
        {
            enemy.atta = false;
            if (i % 4 == 0)
            {
                enemy.attack.Elastick(elastickrange, elastickspeed, elastickreturnspeed);
            }
            else
            {
                StartCoroutine(enemy.attack.shockwave(shockwavespeed, shockwavezise, shockwaverange));
                StartCoroutine(enemy.attack.shockwave(shockwavespeed, shockwavezise, shockwaverange));
            }
            i++;
            resetpositon();
        }
        else if (enemy.atta && !enemy.volnereble)
        {
            enemy.agent.SetDestination(enemy.target.position);
        }
        else
        {
            resetpositon();
        }
    }

}
