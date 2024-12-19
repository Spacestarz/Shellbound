using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class urchin : MonoBehaviour
{
    AudioSource sorce;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip landing;

    private Rigidbody rb;
    private int strenght = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sorce = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthSystem>().TakeDamage(1);
        }   
        sorce.PlayOneShot(landing);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dart") || other.CompareTag("Harpoon"))
        {
            sorce.PlayOneShot(death);
            GetComponent<HealthSystem>().TakeDamage(1);   
        }

        if (other.gameObject.CompareTag("Wave"))
        {
            rb.AddForce(transform.up * strenght, ForceMode.Impulse);
        }
    }
    private void OnDestroy()
    {
        
    }
}
