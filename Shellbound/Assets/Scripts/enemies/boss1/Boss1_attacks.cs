using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_attacks : base_enemi_attack
{
    Rigidbody clawrig;
    public GameObject claw;
    float dis;
    public bool velo = false;
    public float firespeed = 4;
    public float returnspeed = 10;
    private void Awake()
    {
        claw = transform.GetChild(0).gameObject;
        clawrig = claw.GetComponent<Rigidbody>();
        clawrig.constraints = RigidbodyConstraints.FreezeAll;
        clawrig.useGravity = false;
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Melee();
        }
        transform.LookAt(target);
        dis = Vector3.Distance(transform.position, claw.transform.position);
        if (dis >= 12)
        {
            velo = true;
        }
        if (velo == true)
        {

            claw.transform.position = Vector3.MoveTowards(claw.transform.position, transform.position, returnspeed * Time.deltaTime);

            if (dis < 1)
            {
                claw.transform.position = transform.position;
                clawrig.constraints = RigidbodyConstraints.FreezeAll;

                BeInvisible();
                velo = false;
            }
        }
    }
    public void Elastick()
    {
        BeVisible();
        claw.transform.position = transform.position;

        clawrig.constraints = RigidbodyConstraints.None;

        clawrig.velocity = transform.forward * firespeed;

    }

    private void BeVisible()
    {
        var renderer = claw.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }
    }

    private void BeInvisible()
    {

        var renderer = claw.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
    }
}
