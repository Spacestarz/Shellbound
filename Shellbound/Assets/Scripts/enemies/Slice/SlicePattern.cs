using System.Collections.Generic;
using UnityEngine;

public class SlicePattern : MonoBehaviour
{
    Fire fire;
    public int totalSliced = 0;
    public int sliceAmount = 3;

    public List<SliceTarget> possibleArrows;
    SliceTarget currentArrow;
    public SliceTarget spawnedArrow;

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

    public void DestroyArrow()
    {
        Destroy(spawnedArrow.gameObject);
        totalSliced++;
    }

    public void NextSliceArrow()
    {
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
            PlayerSlice.SetTargetDirection(currentArrow.direction);
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

    public void ResetPattern()
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
