using System.Collections.Generic;
using UnityEngine;

public class SlicePattern : MonoBehaviour
{
    Fire fire;
    
    public int totalSliced = 0;
    public int sliceAmount = 3;

    public List<SliceTarget> arrows;
    public List<SliceTarget> possibleArrows;

    SliceTarget currentArrow;
    public SliceTarget spawnedArrow;

    SliceableObject parentSlice;

    AudioSource audioSource;
    public AudioClip sliceStart;
    public AudioClip sliceFinish;

    public SliceAnimate sliceAnimation;
    public SliceAnimate spawnedSliceAnimation;


    private void Awake()
    {
        fire = FindObjectOfType<Fire>();
        parentSlice = GetComponentInParent<SliceableObject>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void DestroyArrow()
    {
        if(spawnedArrow)
        {
            Destroy(spawnedArrow.gameObject);
        }

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
                i = Random.Range(0, arrows.Count - 1);
                currentArrow = arrows[i];
                FindPossibleArrows(i);
            }
            else
            {
                int currentArrowIndex = arrows.IndexOf(currentArrow);
                FindPossibleArrows(currentArrowIndex);
                
                i = Random.Range(0, possibleArrows.Count - 1);
                currentArrow = possibleArrows[i];
            }
            

            PlayerSlice.SetTargetDirection(currentArrow.direction);
            spawnedArrow = Instantiate(currentArrow, transform);
            
            spawnedSliceAnimation = Instantiate(sliceAnimation, transform);
        }
        else
        {
            parentSlice.FinalSlice();
            fire.ReturnHarpoon();
            PlayerSlice.SetSliceMode(false);
            StartCoroutine(Camera.main.GetComponent<RotateCamera>().SetCameraLock(false));
            
            ResetPattern();
        }
    }

    void MoveToEnd(int i)
    {
        SliceTarget value = arrows[i];
        arrows.RemoveAt(i);
        arrows.Add(value);
    }

    void FindPossibleArrows(int i)
    {
        possibleArrows.Clear();

        switch(i)
        {
            case 0:
                SetPossibleArrows(1, 2, 3, 6, 7);
                break;
            case 1:
                SetPossibleArrows(0, 2, 3, 4, 5);
                break;
            case 2:
                SetPossibleArrows(0, 1, 3, 5, 7);
                break;
            case 3:
                SetPossibleArrows(0, 1, 2, 4, 6);
                break;
            case 4:
                SetPossibleArrows(1, 3, 7);
                break;
            case 5:
                SetPossibleArrows(1, 2, 6);
                break;
            case 6:
                SetPossibleArrows(0, 3, 5);
                break;
            case 7:
                SetPossibleArrows(0, 2, 4);
                break;
        }
    }

    void SetPossibleArrows(int a, int b, int c, int d, int e)
    {
        int[] ints = {a, b, c, d, e};

        foreach (int i in ints)
        {
            possibleArrows.Add(arrows[i]);
        }
    }

    void SetPossibleArrows(int a, int b, int c)
    {
        int[] ints = {a, b, c};
        
        foreach (int i in ints)
        {
            possibleArrows.Add(arrows[i]);
        }
    }

    public void ResetPattern()
    {
        totalSliced = 0;
    }

    public void FailPattern()
    {
        if(spawnedSliceAnimation)
        {
            Destroy(spawnedSliceAnimation.gameObject);
        }
        fire.ReturnHarpoon();
        ResetPattern();
        StartCoroutine(Camera.main.GetComponent<RotateCamera>().SetCameraLock(false));
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
