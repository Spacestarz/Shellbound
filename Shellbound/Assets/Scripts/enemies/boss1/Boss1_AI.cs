using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_AI : Base_enemy
{
    
    void Update()
    {
        //kollar om ett objekt som har en layer definerad i player är inom en svere av diametern som defineras av attackrange.
        if (Range(7))
        {
            agent.SetDestination(transform.position);
            //transform.LookAt(target);
            if (!cooling)
            {
                StartCoroutine(Cool());
            }
        }
        else if (Range(8) && atta)
        {
            agent.SetDestination(transform.position);
            atta = false;
            StartCoroutine(attack.shockwave(3, 3, 15));
        }
        else if (Range(12) && atta)
        {
            agent.SetDestination(transform.position);
            attack.Elastick();
            atta = false;
        }
        else
        {
            //seger åt agent componenten att gå mot punkten definerad i target.position
            agent.SetDestination(target.position);
        }

    }
    
}
