using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Shark_Phase_1 : Phase
{
    

    public int fart;
    
    public override void phase()
    {
        // Logic for phase 1
        //Debug.Log("test");
        if (enemy.Range(10) && enemy.atta && !enemy.volnereble && !PlayerSlice.SliceMode())
        {
            enemy.atta = false;
            attacks.Dashattack();
        }
        else if (enemy.Range(28) && enemy.atta && !enemy.volnereble && !PlayerSlice.SliceMode())
        {
            enemy.atta = false;
            attacks.shockwave();
        }
        else if(!enemy.volnereble && !PlayerSlice.SliceMode())
        {
            Move();
        }
    }


}
