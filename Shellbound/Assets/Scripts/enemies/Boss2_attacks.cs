using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Boss2_attacks : MonoBehaviour
{

    private float damage = 12;

    public AOEattack aoeattack;

    public bool aoeattackGO = false;

    //TODO
    // Make the boss spin? or att least make an aoe attack that knock back the player


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Dashattack();

        Mouthattack();

        
        if (Input.GetKeyDown(KeyCode.B))
        {
            Aoeattack();
          
        }

    }

    public void Dashattack()
    {

    }

    public void Mouthattack()
    {

    }

    public void Aoeattack()
    {
        aoeattackGO = true;
        Debug.Log("aoeattackhere");
    }

   

}
