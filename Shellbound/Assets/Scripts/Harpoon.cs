using UnityEngine;

public class Harpoon : MonoBehaviour
{
    Rigidbody rb;

    public Fire fire;

    public bool collisionHIT = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void OnTriggerEnter(Collider collisioncheck)
    {
        Debug.Log("Wi");
        if (collisioncheck.CompareTag("Enemy") || fire.goingAway)
        {
            Debug.Log("Wa");
            collisionHIT = true;

            // Find the closest point on the collided object's surface to the rope
            Vector3 closestPoint = collisioncheck.ClosestPoint(transform.position);

            // Log the closest point for debugging
            Debug.Log("Closest point on collision surface: " + closestPoint);

            // Move the rope to this closest point
            //make a lerp to make it more smooth?
            transform.position = closestPoint;
            rb.constraints = RigidbodyConstraints.FreezeAll;

            // Optionally, stop further rope movement or implement other logic
            Debug.Log("Rope stuck at: " + closestPoint);
        }
    }
}
