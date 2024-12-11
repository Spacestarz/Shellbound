using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Shark_Phase_1 : Phase
{
    

    public int fart;
    
    public override void phase()
    {
        // Logic for phase 1
        if (enemy.Range(28))
        {
            attacks.shockwave();
        }

        if(Time.deltaTime < 5)
        {
            
        }
    }


}
