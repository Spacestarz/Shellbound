using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Shark_Phase_2 : Phase
{
    int amount = 1;
    // Start is called before the first frame update
    public override void phase()
    {
        if (enemy.Range(10) && enemy.atta && !enemy.volnereble && !PlayerSlice.SliceMode())
        {
            enemy.atta = false;
            attacks.Dashattack();
        }
        else if (enemy.Range(28) && enemy.atta && !enemy.volnereble)
        {
            enemy.atta = false;
            if (i % 4 == 0)
            {
                attacks.shockwave();
                //attack.Elastick(elastickrange, elastickspeed, elastickreturnspeed);
                //StartCoroutine(elestickdelay(elastickdelai));
            }
            else
            {
                //attack.parent.stop();
                //attack.still = true;
                //attack.shockwave(shockwavespeed, shockwavezise, shockwaverange);
                //StartCoroutine(cooldown(shockwavespeed));
                //amount = Random.Range(1, 2);
                StartCoroutine(bite(amount));


            }
            i++;
            resetpositon();
        }
        else if (!enemy.volnereble && !PlayerSlice.SliceMode())
        {
            Move();
        }
    }
    IEnumerator bite(int amount)
    {
        for (int j = 0; j <= amount; j++)
        {
            yield return new WaitForSeconds(1.0f);
            attacks.Mouthattack();
        }
        enemy.attacking();
    }
}
