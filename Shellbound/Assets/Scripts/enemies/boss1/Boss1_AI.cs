using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_AI : MonoBehaviour
{
    //statemachine
    public List<base_enemi_attack> phases;
    public base_enemi_attack phase;
    Enemi_Health health;

    private void Awake()
    {
        phase = phases[0];
        health = GetComponent<Enemi_Health>();
    }
    private void Update()
    {
        phase.phase();
        if (health.currentHP < 10)
        {
            phase = phases[2];
        }
        else if (health.currentHP < 30)
        {
            //Debug.Log("les then 10");
            phase = phases[1];
        }
    }


}
