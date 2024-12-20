using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projektile : MonoBehaviour
{
    // Start is called before the first frame update
    int damage = 1;
    public BossAttacksCommon attack;
    private void Awake()
    {
        attack = GetComponentInParent<BossAttacksCommon>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (transform.CompareTag("weakpoint") && !other.CompareTag("Enemy"))
        {
            attack.velo = true;
        }
        if (other.CompareTag("Player"))
        {
            if(!CompareTag("Wave"))
            {
                other.GetComponent<HealthSystem>().TakeDamage(damage);
            }
            else if(other.GetComponent<PlayerController>().grounded)
            {
                other.GetComponent<HealthSystem>().TakeDamage(damage);
            }
        }
    }
}
