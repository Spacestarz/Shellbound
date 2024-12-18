using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class ShrimpCrowd : MonoBehaviour
{
  
    public bool IsCheering;
    public bool IsBooing;

    public AudioSource cheerAudioSource;  
    public AudioClip cheerSound;

    // cheer / boo when player complete a full 3 slice

    /*
     * have a scrimp script where they cheer and this is the central hub to see if the bool is still true
     * 
     * */
    public void Update()
    {

        if (!cheerAudioSource.isPlaying)
        {
            IsCheering = false;
        }
    }

    public void Cheer()
    {       
       IsCheering = true;
       Debug.Log("cheering is" + IsCheering);
       cheerAudioSource.PlayOneShot(cheerSound);

    }

    public void Boo()
    {
        IsBooing = true;
    }
}
