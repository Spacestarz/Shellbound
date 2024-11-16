using UnityEngine;

public class base_enemi_attack : MonoBehaviour
{
    RaycastHit ray;
    int damage = 1;
    bool ready = false;
    public float range = 5;
    public float pushForce = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Melee();
        }
    }
    public void Melee()
    {
        if(Physics.SphereCast(gameObject.transform.position, 1, transform.forward, out ray, range))
        {
            
            Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * 5, Color.red, 5);
            if (ray.collider.gameObject.tag == "Player")
            {
                Debug.Log("hit");
                ray.collider.GetComponent<HealthSystem>().TakeDamage(damage);
                ray.collider.GetComponent<Rigidbody>().velocity = transform.forward * pushForce;
            }

        }
    }
}
