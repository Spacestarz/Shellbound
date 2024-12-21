using UnityEngine;

public class FireDart : MonoBehaviour
{
    bool fireRequested;
    [HideInInspector] public bool shot;

    public GameObject dart;

    [Header("audio")]
    AudioSource source;
    public AudioClip dartSound;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if(fireRequested)
        {
            Fire();
            fireRequested = false;
        }
    }

    public void RequestFire()
    {
        fireRequested = true;
    }

    void Fire()
    {
        source.PlayOneShot(dartSound, 0.7f);
        shot = true;
        Instantiate(dart, Camera.main.transform.position, Camera.main.transform.rotation);
    }

    void FireRayCast()
    {
        source.PlayOneShot(dartSound, 0.5f);

    }
}
