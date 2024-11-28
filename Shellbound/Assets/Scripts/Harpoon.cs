using UnityEngine;

public class Harpoon : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody rb;
    HarpoonLine line;
    
    public GameObject caughtObject;
    public Fire fire;

    public bool collisionHIT = false;
    public static bool hasCaught;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponentInChildren<SpriteRenderer>();
        line = GetComponentInChildren<HarpoonLine>();
        sr.enabled = false;
        hasCaught = false;
    }


    public void OnTriggerEnter(Collider collisionCheck)
    {
        if (fire.goingAway && collisionCheck.CompareTag("Enemy") || collisionCheck.CompareTag("weakpoint") || collisionCheck.CompareTag("TutorialHookThis"))
        {
            HarpoonHit(collisionCheck);
            //fire.goingAway = false;
            //collisionHIT = true;

            //caughtObject = collisioncheck.gameObject;
            //PlayerSlice.SetCaughtObject(caughtObject);
            //hasCaught = true;

            //SetVisibility(false);
            //caughtObject.GetComponent<Enemi_Health>().DisableAI();

            //// Find the closest point on the collided object's surface to the rope
            //Vector3 closestPoint = collisioncheck.ClosestPoint(transform.position);

            //// Move the rope to this closest point
            ////make a lerp to make it more smooth?
            //transform.position = closestPoint;
            //rb.constraints = RigidbodyConstraints.FreezeAll;

            //// Optionally, stop further rope movement or implement other logic
            //Debug.Log("Rope stuck at: " + closestPoint);
        }
        //else if (collisionCheck.CompareTag("weakpoint") && fire.goingAway)
        //{
        //    //StartCoroutine(collisionCheck.transform.parent.parent.GetComponent<Base_enemy>().weekTimer());
        //    //fire.ReturnHarpoon();
        //}
        else if (fire.goingAway)
        {
            fire.ReturnHarpoon();
        }
    }


    void HarpoonHit(Collider collisionCheck)
    {
        //Every tag that bounces the harpoon back, put into this "or"
        if (!collisionCheck.CompareTag("weakpoint"))
        {
            if (collisionCheck.CompareTag("Enemy") && !collisionCheck.GetComponent<Base_enemy>().volnereble)
            {
                fire.ReturnHarpoon();
            }
            else
            {
                fire.goingAway = false;
                collisionHIT = true;

                caughtObject = collisionCheck.gameObject;
                PlayerSlice.SetCaughtObject(caughtObject);
                hasCaught = true;
                SetVisibility(false);
            
                // Find the closest point on the collided object's surface to the rope
                Vector3 closestPoint = collisionCheck.ClosestPoint(transform.position);

                // Move the rope to this closest point
                //make a lerp to make it more smooth?
                transform.position = closestPoint;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
        if (collisionCheck.CompareTag("Enemy") && collisionCheck.GetComponent<Base_enemy>().volnereble)
        {
            caughtObject.GetComponent<Enemi_Health>().DisableAI();
        }

        else if (collisionCheck.CompareTag("TutorialHookThis"))
        {
            Debug.Log("HIHO TUTORIAL");

        }

        else if(collisionCheck.CompareTag("weakpoint") && fire.goingAway)
        {
            //Debug.Log("hit");
            StartCoroutine(collisionCheck.transform.parent.parent.GetComponent<Base_enemy>().weekTimer());
            fire.ReturnHarpoon();
        }
    }

    public void SetVisibility(bool visibility)
    {
        sr.enabled = visibility;
        if (!hasCaught)
        {
            line.SetEnabled(visibility);
        }
    }
}
