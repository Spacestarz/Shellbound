using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float MaxHp;
    public float currentHP;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageTaken)
    {
        currentHP -= damageTaken;  
    }
}
