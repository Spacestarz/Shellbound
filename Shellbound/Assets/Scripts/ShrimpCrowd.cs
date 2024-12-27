using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class ShrimpCrowd : MonoBehaviour
{

    public bool IsCheering;
    public bool IsBooing;
    public bool timeToReturn;
   

    [HideInInspector] public AudioSource cheerAudioSource;
    public List<AudioClip> cheerSound;

    public float currentAudioLength;

    private void Awake()
    {
        DOTween.SetTweensCapacity(5000, 10);

        cheerAudioSource = GetComponent<AudioSource>();
        cheerAudioSource.volume = 0.5f;
    }

    private void Update()
    {
        
    }

    public void Cheer()
    {
        IsCheering = true;
        timeToReturn = true;

        cheerAudioSource.Stop();
        cheerAudioSource.pitch = Random.Range(0.85f, 1.15f);
        cheerAudioSource.clip = cheerSound[Random.Range(0, cheerSound.Count)];
        cheerAudioSource.Play();

        Invoke(nameof(StopCheer), cheerAudioSource.clip.length);
    }

    void StopCheer()
    {
        IsCheering = false;
    }

    public void Boo()
    {
        IsBooing = true;
    }
}
