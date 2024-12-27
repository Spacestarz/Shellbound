using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class ShrimpCrowd : MonoBehaviour
{

    public bool IsCheering;
    public bool IsBooing;
    public bool timeToReturn;
   
    private ShrimpMove ShrimpMoveScript;

    public AudioSource cheerAudioSource;
    public List<AudioClip> cheerSound;

    private void Awake()
    {
        DOTween.SetTweensCapacity(5000, 10);
        ShrimpMoveScript = GetComponentInChildren<ShrimpMove>();
        cheerAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
    }

    public void Cheer()
    {
        IsCheering = true;
        timeToReturn = true;
        cheerAudioSource.pitch = Random.Range(0.9f, 1.1f);
        cheerAudioSource.PlayOneShot(cheerSound[Random.Range(0, cheerSound.Count)], 0.5f);
    }

    public void Boo()
    {
        IsBooing = true;
    }
}
