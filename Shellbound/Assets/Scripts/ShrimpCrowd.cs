using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class ShrimpCrowd : MonoBehaviour
{
  
    public bool IsCheering;
    public bool IsBooing;

    private ShrimpMove ShrimpMoveScript;

    AudioSource cheerAudioSource;  
    public List<AudioClip> cheerSound;

    private void Awake()
    {
        ShrimpMoveScript = GetComponentInChildren<ShrimpMove>();
        cheerAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {      
        if (!cheerAudioSource.isPlaying)
        {         
            IsCheering = false;
            ShrimpMoveScript.ReturnToStartPos();
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
