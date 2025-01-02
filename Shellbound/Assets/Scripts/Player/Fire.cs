using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    Rigidbody harpoonRigidBody;
    Harpoon harpoon;
    MovingLine harpoonLine;
    WeaponAnimator harpoonUI;

    public GameObject Anchor;
    public GameObject harpoonObject;
    public GameObject mainCam;
    public float fireRate = 2;
    public float speedReturn = 1;
    public ParticleSystem fire;

    float dist;
    public float maxDistancefromAnchor = 15f; //test this to see whats best

    //bools
    public bool fired = false;
    public bool goingAway = false;

    AudioSource source;
    public AudioClip harpoonSound;

    public HarpoonAnimator harpoonAnimator;

    void Awake()
    {
        harpoon = harpoonObject.GetComponent<Harpoon>();
        harpoonRigidBody = harpoonObject.GetComponent<Rigidbody>();
        harpoonLine = harpoonObject.GetComponent<MovingLine>();

        harpoonRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        harpoonRigidBody.useGravity = false;

        source = GetComponent<AudioSource>();
    }


    void Update()
    {
        //distance of the Anchor and rope
        dist = Vector3.Distance(Anchor.transform.position, harpoonObject.transform.position);
        if (dist > maxDistancefromAnchor && goingAway)
        {
            ReturnHarpoon();
        }
    }

    public void InvokeFire()
    {
        fired = true;
        Invoke(nameof(FireHarpoon), 0.189f);
    }

    public void FireHarpoon()
    {
        source.PlayOneShot(harpoonSound);
        fire.Play();
        //fire.Stop();
        harpoonObject.SetActive(true);
        harpoon.SetVisibility(true);
        goingAway = true;

        harpoonAnimator.Fire();

        harpoonObject.transform.position = Anchor.transform.position;

        harpoonRigidBody.constraints = RigidbodyConstraints.None;

        //fired = true;

        if (harpoonObject.GetComponent<Harpoon>().collisionHIT == false)
        {
            //It move the direction of the main cameras z axis
            harpoonRigidBody.velocity = mainCam.transform.forward * fireRate;
        }
    }


    public void ReturnHarpoon()
    {
        if (Harpoon.hasCaught)
        {
            Harpoon.hasCaught = false;
        }

        if (PlayerSlice.SliceMode())
        {
            PlayerSlice.SetSliceMode(false);
        }

        goingAway = false;
        harpoon.SetVisibility(true);
        harpoon.collisionHIT = false;
        harpoonRigidBody.velocity = Vector3.zero;

        if (harpoon.caughtObject)
        {
            PlayerSlice.ClearCaughtObject();

            if(harpoon.caughtObject.CompareTag("Enemy"))
            {
                harpoon.caughtObject.GetComponent<Enemi_health>().EnableAI();
                harpoon.caughtObject.GetComponent<Base_enemy>().volnereble = false;
            }

            harpoon.caughtObject = null;
        }

        if (gameObject.activeSelf)
        {
            StartCoroutine(nameof(MoveHarpoonBack));
        }
    }


    IEnumerator MoveHarpoonBack()
    {
        while (dist >= 1)
        {
            harpoonObject.transform.position = Vector3.Lerp(harpoonObject.transform.position, Anchor.transform.position, speedReturn * Time.deltaTime);
            yield return null;
        }

        harpoonObject.SetActive(false);

        harpoonObject.transform.position = Anchor.transform.position;
        harpoonRigidBody.constraints = RigidbodyConstraints.FreezeAll;

        harpoon.SetVisibility(false);
        fired = false;

        yield break;
    }
}
