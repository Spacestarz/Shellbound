using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_AI : Base_enemy
{
    void Update()
    {
        //kollar om ett objekt som har en layer definerad i player �r inom en svere av diametern som defineras av attackrange.
        if (!Range(attackRange))
        {
            Debug.Log("test");
            //seger �t agent componenten att g� mot punkten definerad i target.position
            agent.SetDestination(target.position);
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
    }

}
