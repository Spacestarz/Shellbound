using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projektile : MonoBehaviour
{
    // Start is called before the first frame update
    int damage = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthSystem>().TakeDamage(damage);
        }
    }
}
