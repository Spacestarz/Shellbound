using System.Collections.Generic;
using UnityEngine;

public class SlicePattern : MonoBehaviour
{
    Fire fire;
    public int totalSliced = 0;
    public int sliceAmount = 3;

    public List<SliceTarget> possibleArrows;
    SliceTarget currentArrow;
    SliceTarget spawnedArrow;

    Enemi_Health parentHealth;

    AudioSource audioSource;
    public AudioClip sliceStart;
    public AudioClip sliceFinish;

    private void Awake()
    {
        parentHealth = GetComponentInParent<Enemi_Health>();
        audioSource = GetComponentInParent<AudioSource>();
        fire = FindObjectOfType<Fire>();
    }

    private void LateUpdate()
    {
        if (PlayerSlice.activatedThisFrame)
        {
            Debug.Log("Pattern 31, spawned arrow" + spawnedArrow);
        }
        if (PlayerSlice.activatedThisFrame && !spawnedArrow)
        {
            Debug.Log("Pattern 39");
            NextSliceArrow();
        }
        else if (PlayerSlice.deactivatedThisFrame)
        {
            if (spawnedArrow)
            {
                DestroyArrow();
            }
            ResetPattern();
        }
    }

    public void DestroyArrow()
    {
        Destroy(spawnedArrow.gameObject);
    }

    public void NextSliceArrow()
    {
        Debug.Log("Pattern 59");
        if (spawnedArrow)
        {
            DestroyArrow();
        }

        if (totalSliced < sliceAmount)
        {
            int i;

            if (currentArrow == null)
            {
                i = Random.Range(0, possibleArrows.Count - 1);
            }
            else
            {
                i = Random.Range(0, possibleArrows.Count - 2);
            }

            currentArrow = possibleArrows[i];

            MoveToEnd(i);
            spawnedArrow = Instantiate(currentArrow, transform);
        }

        else
        {
            parentHealth.TakeDamage(5);
            fire.ReturnHarpoon();
            PlayerSlice.SetSliceMode(false);

            ResetPattern();
        }
    }

    void MoveToEnd(int i)
    {
        SliceTarget value = possibleArrows[i];
        possibleArrows.RemoveAt(i);
        possibleArrows.Add(value);
    }

    void ResetPattern()
    {
        totalSliced = 0;
    }

    public void PlayAudio(string audio)
    {
        if (audio == "start")
        {
            audioSource.PlayOneShot(sliceStart);
        }
        else if (audio == "finish")
        {
            audioSource.PlayOneShot(sliceFinish);
        }
    }
}
