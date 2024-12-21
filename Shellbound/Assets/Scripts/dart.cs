using System.Collections;
using UnityEngine;

public class dart : MonoBehaviour
{
    public FireDart fireDart;
    public float speed = 2;
    public float destroytime = 3;
    Rigidbody rig;

    // Start is called before the first frame update
    private void Awake()
    {
        fireDart = Camera.main.transform.GetComponentInParent<FireDart>();
        rig = GetComponent<Rigidbody>();
        fire();
    }
    private void fire()
    {
        Vector3 test = Camera.main.transform.forward;
        rig.velocity = test * speed;
        Debug.DrawRay(transform.position,test * speed,Color.red,10);
        StartCoroutine(destroy());
    }
    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(destroytime);
        Destroy();
    }
    public void Destroy()
    {
        fireDart.hasShot = false;
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Destroy();
        }
    }
}
