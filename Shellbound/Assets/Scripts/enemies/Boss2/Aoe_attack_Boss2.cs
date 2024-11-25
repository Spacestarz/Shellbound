using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;


public class Aoe_attack_Boss2 : MonoBehaviour
{

    //testing spere thing
    private float radius = 10f;
    private int damage = 12;

    public Rigidbody rbGoblinShark;
    private Rigidbody rbplayer;
    private Collider playerCollider;
    public int KnockbackStrenght;

    public GameObject player;
    public GameObject Boss2;

    private GameObject Aoeindicator;
    public bool aoeattackGO = false;
    public HealthSystem healthSystem;

    SpriteRenderer sr;
    public bool visibility = false;

    //testing DO TWEEN BELOW
    Vector3 aoeComplete;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        
        rbplayer = player.GetComponent<Rigidbody>();
        playerCollider = player.GetComponent<Collider>();

        Aoeindicator = GameObject.Find("AOE indicator BOSS2");
        Aoeindicator.SetActive(false);

        //TESTING DO TWEEN
        aoeComplete = transform.localScale;
        transform.DOScale(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Aoeindicator.SetActive(true);
            Invoke("Aoeattack", 2.0f);
            transform.DOScale(0, 0);
            transform.DOScale(aoeComplete, 2f);
            SetVisibility(true);

            /*
             * MY TINY NOTES 
            när det gäller inside circle animation så använd dotween 
            i start säg att den blir scale 0 och sen kan den expandera till vart den ska vara i. 
            kan alla lite JUICE som att det är vågor eller transparency.

            */
        }
    }

    public void Aoeattack()
    {
        //TODO
        //Fix when this will go off in the combat
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        Aoeindicator.SetActive(false);
        SetVisibility(false);

        if ((hitColliders.Any(hitCollider => hitCollider.CompareTag("Player"))))
        {

            Vector3 direction = new Vector3(playerCollider.transform.position.x - transform.position.x,
              0f,
             playerCollider.transform.position.z - transform.position.z).normalized;

            rbplayer.AddForce(direction * KnockbackStrenght * 10, ForceMode.Impulse);

            // knockback is visualised in scene mode
            Debug.DrawRay(transform.position, direction * 10f, Color.red, 10f);

            Debug.Log("PLAYER take dmg AUCH");
            healthSystem.TakeDamage(damage);

        }
   

    }

    private void OnDrawGizmos()
    {
        //DRAWS AREA OF AOE ATTACK
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, radius);

        Debug.Log("drawing blue lol");
    }



    public void SetVisibility(bool visibility)
    {
        sr.enabled = visibility;
    }


}
