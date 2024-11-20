using UnityEngine;

public class Harpoon : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody rb;
    
    public GameObject caughtObject;
    public Fire fire;

    public bool collisionHIT = false;
    public static bool hasCaught;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponentInChildren<SpriteRenderer>();
        sr.enabled = false;
    }


    public void OnTriggerEnter(Collider collisioncheck)
    {

        if (collisioncheck.CompareTag("Enemy") && fire.goingAway)
        {
            collisionHIT = true;

            caughtObject = collisioncheck.gameObject;
            hasCaught = true;

            SetVisibility(false);
            caughtObject.GetComponent<Enemi_Health>().DisableAI();

            // Find the closest point on the collided object's surface to the rope
            Vector3 closestPoint = collisioncheck.ClosestPoint(transform.position);

            // Log the closest point for debugging

            // Move the rope to this closest point
            //make a lerp to make it more smooth?
            transform.position = closestPoint;
            rb.constraints = RigidbodyConstraints.FreezeAll;

            // Optionally, stop further rope movement or implement other logic
            Debug.Log("Rope stuck at: " + closestPoint);
        }
    }

    public void SetVisibility(bool visibility)
    {
        sr.enabled = visibility;
    }
}
