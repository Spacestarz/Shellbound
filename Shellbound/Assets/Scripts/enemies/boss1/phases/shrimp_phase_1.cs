using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrimp_phase_1 : base_enemi_attack
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
            attack.Elastick(elastickrange, elastickspeed, elastickreturnspeed);
            enemy.atta = false;
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
