using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dart : MonoBehaviour
{
    public PlayerController controller;
    public float speed = 2;
    public float destroytime = 3;
    Rigidbody rig;
    // Start is called before the first frame update
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        fire();
        controller = Camera.main.transform.GetComponentInParent<PlayerController>();
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
        controller.shot = false;
        Destroy(this.gameObject);
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
