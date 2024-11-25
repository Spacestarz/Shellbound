using UnityEngine;
using UnityEngine.AI;

public class base_enemi_attack : MonoBehaviour
{
    RaycastHit ray;
    int damage = 1;
    bool ready = false;
    //public float range = 5;
    public float pushForce = 100;
    public Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    public void Start()
    {
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    
    public void Melee(float range)
    {
        if(Physics.SphereCast(gameObject.transform.position, 1, transform.forward, out ray, range))
        {
            
            Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * 5, Color.red, 5);
            if (ray.collider.gameObject.tag == "Player")
            {
                Debug.Log("hit");
                ray.collider.GetComponent<HealthSystem>().TakeDamage(damage);
                //ray.collider.GetComponent<Rigidbody>().velocity = transform.forward * pushForce;
                ray.collider.GetComponent<Rigidbody>().AddForce(transform.forward * pushForce);
            }

        }
    }

}
