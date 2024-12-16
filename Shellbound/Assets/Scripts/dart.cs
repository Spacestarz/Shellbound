using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dart : MonoBehaviour
{
    public float speed = 2;
    Rigidbody rig;
    // Start is called before the first frame update
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        fire();
    }
    private void fire()
    {
        rig.velocity = transform.rotation.z * speed;
        StartCoroutine(destroy());
    }
    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
