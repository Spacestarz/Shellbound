using Spine.Unity.Examples;
using UnityEngine;

public class urchin : MonoBehaviour
{   //       ↑
    //       U should be capitalized
    //    source*
    //      ↓
    AudioSource sorce;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip landing;

    private Enemi_health enemiHealthScript;
    private Rigidbody rb;
    private SpriteRenderer sr;

    [SerializeField] Sprite[] sprites;

    private int strenght = 10;

    private void Awake()
    {
        enemiHealthScript = GetComponent<Enemi_health>();
        rb = GetComponent<Rigidbody>();
        sr = GetComponentInChildren<SpriteRenderer>();
        sorce = GetComponent<AudioSource>();

        sr.sprite = sprites[0];
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthSystem>().TakeDamage(1);
            sorce.PlayOneShot(death);
            enemiHealthScript.TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            sorce.PlayOneShot(landing);
            sr.sprite = sprites[1];
        }
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
            sr.sprite = sprites[0];
        }
    }
}
