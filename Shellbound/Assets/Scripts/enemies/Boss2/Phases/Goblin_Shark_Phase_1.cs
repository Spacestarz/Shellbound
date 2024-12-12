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
        if (enemy.Range(WaveDistance) && enemy.atta && !enemy.volnereble && !PlayerSlice.SliceMode())
        {
            enemy.atta = false;
            attacks.shockwave(WaveDistance, WaveDuration);
        }
        else if (enemy.Range(DashRange) && enemy.atta && !enemy.volnereble && !PlayerSlice.SliceMode())
        {
            enemy.atta = false;
            attacks.Dashattack(DashDistance,DashDuration);
        }
        else if(!enemy.volnereble && !PlayerSlice.SliceMode())
        {
            Move();
        }
    }


}
