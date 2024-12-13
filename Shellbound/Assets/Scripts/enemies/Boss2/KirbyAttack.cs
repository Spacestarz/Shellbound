using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KirbyAttack : MonoBehaviour
{
    public GameObject player;
    RaycastHit ray;
    [Header("Radius of kirby attack")]
    [SerializeField] int radius = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            TestingKirby();
        }
    }

    //stealing from boss 1 will need to change 
    public void TestingKirby()
    {
        
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in hitColliders)
        {
            // Check if the detected object is the player
            if (collider.gameObject == player)
            {
                Debug.Log("player in area");
                Kirby();
                //TODO MAKE PLAYER GETS PUSHED TO BOSS
                return; 
            }
        }



    }

    private void Kirby()
    {
        //TODO player move towards boss position

        //extra vector i playercontroller som addar mer 
        //normalize vector directtion play with the strenght!
    }

    private void OnDrawGizmos()
    {
        //DRAWS AREA OF kirby attack
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, radius);

        Debug.Log("Drawing kirby area");
    }


}
