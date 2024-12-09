using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrimp_phase_1 : base_enemi_attack
{
    public override void phase()
    {
        if (enemy.Range(startpunchrange) && !enemy.volnereble && !PlayerSlice.SliceMode())
        {
            resetpositon();
            transform.LookAt(new Vector3(attack.target.position.x, transform.position.y, attack.target.position.z));
            if (!attack.cooling)
            {
                StartCoroutine(attack.Cool(punchspeed, punchrange));
            }
        }
        else if (enemy.Range(startelastickrange) && enemy.atta && !enemy.volnereble && !PlayerSlice.SliceMode())
        {
            StartCoroutine(elestickdelay(elastickdelai));
            //attack.Elastick(elastickrange, elastickspeed, elastickreturnspeed);
            //enemy.atta = false;
            //resetpositon();
        }
        else if (!enemy.volnereble && !PlayerSlice.SliceMode())
        {
            enemy.agent.SetDestination(enemy.target.position);
        }
    }
}
