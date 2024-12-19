using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class urchin : MonoBehaviour
{
    AudioSource sorce;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip landing;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthSystem>().TakeDamage(1);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dart") || other.CompareTag("Harpoon"))
        {
            GetComponent<HealthSystem>().TakeDamage(1);
            sorce.PlayOneShot(death);
        }
        else
        {
            sorce.PlayOneShot(landing);
        }
    }
}
