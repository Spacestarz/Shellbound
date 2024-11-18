using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEattack : MonoBehaviour
{
    public Boss2_attacks boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = boss.GetComponent<Boss2_attacks>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider collisioncheck)
    {

        boss.Aoeattacks();
        
       Debug.Log("Regular AOE attack no shockwave");
        
        if (collisioncheck.CompareTag("Player"))
        {
            Debug.Log("Auch says the player");
        }


    }
}
