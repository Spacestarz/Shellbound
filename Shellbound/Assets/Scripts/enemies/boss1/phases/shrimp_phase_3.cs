using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrimp_phase_3 : base_enemi_attack
{
    public GameObject wave1;
    public GameObject wave2;
    public override void phase()
    {
        if (enemy.Range(startpunchrange) && !enemy.volnereble && !PlayerSlice.SliceMode())
        {
            resetpositon();
        }
        if (enemy.Range(startelastickrange) && enemy.atta && !enemy.volnereble && !PlayerSlice.SliceMode())
        {
            enemy.atta = false;
            if (i % 4 == 0)
            {
                //attack.Elastick(elastickrange, elastickspeed, elastickreturnspeed);
                StartCoroutine(elestickdelay(elastickdelai));
            }
            else
            {
                //attack.parent.stop();
                attack.still = true;
                StopCoroutine(dublewave(wavemount));
                wavemount = Random.Range(1, 5);
                StartCoroutine(dublewave(wavemount));
            }
            i++;
            resetpositon();
        }
        else if (!enemy.Range(startpunchrange) && !enemy.volnereble && !PlayerSlice.SliceMode())
        {
            enemy.agent.SetDestination(enemy.target.position);
        }
        else
        {
            resetpositon();
        }
        
    }

}
