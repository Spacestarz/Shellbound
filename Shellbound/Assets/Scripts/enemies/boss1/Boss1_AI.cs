using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_AI : Base_enemy
{
    public bool test = false;
    void Update()
    {
        //kollar om ett objekt som har en layer definerad i player är inom en svere av diametern som defineras av attackrange.
        if (!Range(6))
        {
            Debug.Log("test");
            //seger åt agent componenten att gå mot punkten definerad i target.position
            agent.SetDestination(target.position);
        }
        else if (Range(10) && test)
        {
            attack.Elastick();
            test = false;
        }
        else
        {
            agent.SetDestination(transform.position);
            //transform.LookAt(target);
            if (!cooling)
            {
                StartCoroutine(Cool());
            }
        }
        if (attack.velo)
        {
            test = true;
        }
    }

}
