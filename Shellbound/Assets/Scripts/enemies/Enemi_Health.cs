using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemi_Health : HealthSystem
{
    bool Harponed = false;
    Base_enemy enemi;

    private void OnTriggerEnter(Collider other)
    {

    }
    public void DisableAI()
    {
        Harponed = true;
        enemi = GetComponent<Base_enemy>();
        enemi.enabled = false;

    }
}
