using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class ShrimpCrowd : MonoBehaviour
{
  
    public bool IsCheering;
    public bool IsBooing;
    

    public AudioSource cheerAudioSource;  
    public List<AudioClip> cheerSound;

    public void Update()
    {
        if (!cheerAudioSource.isPlaying)
        {
            IsCheering = false;
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            Cheer();
        }
    }

    public void Cheer()
    {       
       IsCheering = true;
       cheerAudioSource.PlayOneShot(cheerSound[Random.Range(0,cheerSound.Count)]);

    }

    public void Boo()
    {
        IsBooing = true;
    }
}
