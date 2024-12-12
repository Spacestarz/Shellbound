using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Shark_Phase_3 : Phase
{
    // Start is called before the first frame update
    public bool kirbyd = false;
    public override void phase()
    {
        if (!kirbyd && enemy.atta && !enemy.volnereble && !PlayerSlice.SliceMode())
        {
            kirbyd = true;
            //kurby goes here
        }
        else if (enemy.Range(DashRange) && enemy.atta && !enemy.volnereble && !PlayerSlice.SliceMode())
        {
            enemy.atta = false;
            attacks.Dashattack(DashDistance, DashDuration);
        }
        else if (!enemy.volnereble && !PlayerSlice.SliceMode())
        {
            Move();
        }
        if (PlayerSlice.SliceMode())
        {
            kirbyd = false;
        }
    }
}
