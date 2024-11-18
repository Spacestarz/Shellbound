using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemi_Health : HealthSystem
{
    bool Harponed = false;
    Base_enemy enemi;
    public float dragspeed = 5;

    private void OnTriggerEnter(Collider other)
    {

    }
    public void DisableAI()
    {
        Harponed = true;
        enemi = GetComponent<Base_enemy>();
        enemi.stop();
        enemi.enabled = false;

    }
    public void EnableAI()
    {
        Harponed = false;
        enemi = GetComponent<Base_enemy>();
        enemi.start();
        enemi.enabled = true;
    }

}
