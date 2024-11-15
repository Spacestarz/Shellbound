using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class base_enemi_attack : MonoBehaviour
{
    RaycastHit ray;
    int damage = 1;
    bool ready = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            male();
        }
    }
    public void male()
    {
        StartCoroutine(windup());
        if(Physics.CapsuleCast(gameObject.transform.position, gameObject.transform.forward * 5, 1, transform.position , out ray))
        {
            
            Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * 5, Color.red, 5);
            if (ray.collider.gameObject.tag == "Player")
            {
                Debug.Log("hit");
                ray.collider.GetComponent<HealthSystem>().TakeDamage(damage);
            }
            ready = false;
        }
    }
    public IEnumerator windup()
    {
        
        yield return new WaitForSeconds(3);
        ready = true;
    }
}
