using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class base_enemi_attack : MonoBehaviour
{
    Rigidbody rig;
    public float shotspeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        rig.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            rig.constraints = RigidbodyConstraints.None;
            rig.velocity = transform.forward * shotspeed * Time.deltaTime;
        }
    }
}
