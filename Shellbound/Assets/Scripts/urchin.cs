using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class urchin : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthSystem>().TakeDamage(1);
        }
    }
}
