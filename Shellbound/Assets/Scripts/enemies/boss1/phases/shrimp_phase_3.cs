using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrimp_phase_3 : base_enemi_attack
{
    public GameObject wave1;
    public GameObject wave2;
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
                attack.Elastick(elastickrange, elastickspeed, elastickreturnspeed);
            }
            else
            {
                attack.parent.stop();
                attack.still = true;
                StartCoroutine(dublewave());
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
        IEnumerator dublewave()
        {
            attack.shockwave(shockwavespeed, shockwavezise, shockwaverange);
            yield return new WaitForSeconds(1);
            attack.shockwave(shockwavespeed, shockwavezise, shockwaverange);
            StartCoroutine(cooldown(shockwavespeed));
        }
    }

}
