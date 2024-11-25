using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_attacks : base_enemi_attack
{
    Rigidbody clawrig;
    public GameObject claw;
    public GameObject wave;
    float dis;
    public bool velo = false;
    //public float firespeed = 4;
    float returnspeed = 10;
    public bool still = false;
    Boss1_AI parent;
    float elastickrange = 12;
    private void Awake()
    {
        claw = transform.GetChild(0).gameObject;
        wave = transform.GetChild(1).gameObject;
        parent = transform.parent.GetComponent<Boss1_AI>();
        clawrig = claw.GetComponent<Rigidbody>();
        clawrig.constraints = RigidbodyConstraints.FreezeAll;
        clawrig.useGravity = false;
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            //Melee();
            StartCoroutine(shockwave(3, 3, 3));
        }
        if (!still)
        {
            transform.LookAt(target);
        }
        dis = Vector3.Distance(transform.position, claw.transform.position);
        if (dis >= elastickrange)
        {
            Debug.Log(dis);
            velo = true;
        }
        if (velo == true)
        {

            claw.transform.position = Vector3.MoveTowards(claw.transform.position, transform.position, returnspeed * Time.deltaTime);

            if (dis < 1)
            {
                claw.transform.position = transform.position;
                clawrig.constraints = RigidbodyConstraints.FreezeAll;

                BeInvisible(claw);
                velo = false;
                parent.start();
                transform.LookAt(target);
                still = false;
                parent.attacking();
            }
        }
    }
    public void Elastick(float range, float firespeed, float returns)
    {
        Debug.Log("testc");
        transform.LookAt(target);
        elastickrange = range;
        returnspeed = returns;
        BeVisible(claw);
        parent.stop();
        still = true;
        //claw.transform.position = transform.position;

        clawrig.constraints = RigidbodyConstraints.None;

        clawrig.velocity = transform.forward * firespeed;

    }

    private void BeVisible( GameObject obj)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }

        var spriteRenderer = obj.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }
    }

    private void BeInvisible(GameObject obj)
    {

        var renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        var spriteRenderer = obj.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }
    public IEnumerator shockwave(float duration, float scale, float range)
    {
        parent.stop();
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        still = true;
        BeVisible(wave);
        Vector3 startscale = wave.transform.localScale;
        Vector3 endscale = Vector3.one;
        endscale.y = endscale.y * scale;
        Vector3 startlocation = wave.transform.localPosition;
        Vector3 endlocation = new Vector3(0,-1.5f,0);
        endlocation.z = endlocation.z + range;
        float elapsed = 0;
        Debug.Log(endlocation);

        while (elapsed < duration)
        {
            var t = elapsed / duration;
            wave.transform.localScale = Vector3.Lerp(startscale, endscale, t);
            wave.transform.localPosition = Vector3.Lerp(startlocation, endlocation, t);
            elapsed += Time.deltaTime;
            yield return null;  
        }
        shockwavereturn();
    }
    void shockwavereturn()
    {
        parent.start();
        still = false;
        BeInvisible(wave);
        wave.transform.localPosition = new Vector3(0,-1.5f,0);
        wave.transform.localScale = new Vector3(1, 0.5f, 1);
        parent.attacking();
    }

}
