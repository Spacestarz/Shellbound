using System.Collections;
using UnityEngine;

public class FireDart : MonoBehaviour
{
    bool fireRequested;
    [HideInInspector] public bool hasShot;
    
    public GameObject dart;

    float cooldownLength = 1.25f;

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
            FireRayCast();
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
        hasShot = true;
        Instantiate(dart, Camera.main.transform.position, Camera.main.transform.rotation);
    }

    void FireRayCast()
    {
        var enemyMask = LayerMask.GetMask("Enemy");
        var weakpointMask = LayerMask.GetMask("WeakPoint");
        var combinedMask = enemyMask | weakpointMask;

        StartCoroutine(Cooldown());

        source.PlayOneShot(dartSound, 0.5f);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, combinedMask);
        CheckHits(hits);
    }


    IEnumerator Cooldown()
    {
        hasShot = true;
        yield return new WaitForSeconds(cooldownLength);
        hasShot = false;
    }

    void CheckHits(RaycastHit[] hits)
    {
        System.Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));

        GameObject firstHit;
        GameObject secondHit;

        if (hits.Length > 0)
        {
            firstHit = hits[0].collider.gameObject;
            if (firstHit.CompareTag("Enemy"))
            {
                if (firstHit.name == "MantisShrimp" && hits.Length > 1)
                {
                    secondHit = hits[1].collider.gameObject;

                    if (firstHit.GetComponent<Boss1_AI>().phase.stunable && secondHit.CompareTag("weakpoint"))
                    {
                        firstHit.GetComponent<Base_enemy>().wekend();
                        //StartCoroutine(secondHit.GetComponent<weekpoint>().MoveCollider());
                    }
                    else
                    {
                        firstHit.GetComponent<Base_enemy>().PlayBonk();
                    }
                }
                else if(firstHit.name == "MantisShrimp")
                {
                    firstHit.GetComponent<Base_enemy>().PlayBonk();
                }
                else if(firstHit.GetComponent<Enemi_health>())
                {
                    firstHit.GetComponent<Enemi_health>().TakeDamage(1);
                }
            }
            else if(firstHit.CompareTag("weakpoint"))
            {
                firstHit.GetComponentInParent<Base_enemy>().wekend();
            }
        }
    }
}
