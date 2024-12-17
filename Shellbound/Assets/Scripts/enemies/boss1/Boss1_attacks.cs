using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Boss1_attacks : BossAttacksCommon
{
    RaycastHit ray;
    int damage = 1;
    bool ready = false;
    //public float range = 5;
    public float pushForce = 100;
    public Transform target;
    NavMeshAgent agent;
    public GameObject wave;
    Rigidbody clawrig;
    public GameObject claw;
    //public GameObject wave;
    float dis;
    
    //public float firespeed = 4;
    float returnspeed = 10;
    public bool still = false;
    public Base_enemy parent;
    float elastickrange = 12;
    bool isfiered = false;
    public bool cooling = false;
        Boss1_AI AI;
    [Header("sound")]
    public AudioSource sorce;
    public AudioClip wavesound;
    public AudioClip elastickstartsound;
    public AudioClip jabsound;
    private void Awake()
    {
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        claw = transform.GetChild(0).gameObject;
        //wave = transform.GetChild(1).gameObject;
        parent = transform.parent.GetComponent<Base_enemy>();
        clawrig = claw.GetComponent<Rigidbody>();
        clawrig.constraints = RigidbodyConstraints.FreezeAll;
        clawrig.useGravity = false;
        sorce = GetComponentInParent<AudioSource>();
        AI = GetComponentInParent<Boss1_AI>();
    }
    public void Update()
    {
       
        if (!still)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
        dis = Vector3.Distance(transform.position, claw.transform.position);
        if (dis >= elastickrange)
        {
            //Debug.Log(dis);
            velo = true;
        }
        if (velo == true)
        {

            claw.transform.position = Vector3.MoveTowards(claw.transform.position, transform.position, returnspeed * Time.deltaTime);

            if (dis < 1)
            {
                claw.transform.position = transform.position;
                clawrig.constraints = RigidbodyConstraints.FreezeAll;

                parent.GetComponentInChildren<MantisAnimator>().anim.SetBool("PunchBool", false);
                BeInvisible(claw);
                velo = false;
                parent.start();
                transform.LookAt(target);
                still = false;
                parent.attacking();
                AI.phase.stunable = false;
            }
        }
    }
    public void Elastick(float range, float firespeed, float returns)
    {
        sorce.PlayOneShot(elastickstartsound, 0.5f);
        transform.LookAt(target);
        elastickrange = range;
        returnspeed = returns;

        parent.GetComponentInChildren<MantisAnimator>().anim.SetBool("PunchBool", true);
        BeVisible(claw);
        //parent.stop();
        still = true;
        //claw.transform.position = transform.position;

        clawrig.constraints = RigidbodyConstraints.None;

        clawrig.velocity = transform.forward * firespeed;

    }

    
    /*public IEnumerator shockwave(float duration, float scale, float range, GameObject wave)
    {
        parent.stop();
        if (isfiered)
        {
            yield return new WaitForSeconds(1);
        }
        isfiered = true;
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
        shockwavereturn(wave);
    }
    void shockwavereturn(GameObject wave)
    {
        parent.start();
        still = false;
        BeInvisible(wave);
        wave.transform.localPosition = new Vector3(0,-1.5f,0);
        wave.transform.localScale = new Vector3(1, 0.5f, 1);
        isfiered = false;
        //parent.attacking();
    }*/
    public void shockwave(float duration, float scale, float range)
    {
        sorce.PlayOneShot(wavesound);
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        Vector3 lokation = transform.position;
        lokation.y = lokation.y - 1.5f;
        //Debug.Log(wave.transform.rotation);
        StartCoroutine(Instantiate(wave,lokation, Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 90))).GetComponent<Wave>().shockwave(duration, scale, range, target));
        Camera.main.GetComponent<CameraHandler>().ShakeCamera();
        
    }
    public IEnumerator Cool(float attackCooling, float attackRange)
    {
        //Debug.Log("run");
        cooling = true;
        yield return new WaitForSeconds(attackCooling);
        if (parent.Range(attackRange) && this.enabled == true)
        {
            Melee(attackRange);
        }
        cooling = false;
    }
    public void Melee(float range)
    {
        if (Physics.SphereCast(gameObject.transform.position, 1, transform.forward, out ray, range))
        {
            Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * 5, Color.red, 5);
            if (ray.collider.gameObject.tag == "Player" && !parent.volnereble)
            {
                sorce.PlayOneShot(jabsound, 4);
                parent.GetComponentInChildren<MantisAnimator>().anim.SetTrigger("Jabbing");
                ray.collider.GetComponent<HealthSystem>().TakeDamage(damage);
                //ray.collider.GetComponent<Rigidbody>().velocity = transform.forward * pushForce;
                //Debug.Log(transform.forward);
                ray.collider.GetComponent<PlayerController>().GetKnockedBack();
                ray.collider.GetComponent<Rigidbody>().AddForce(new Vector3(transform.forward.x, 0,transform.forward.z) * pushForce);
            }

        }
    }

}
