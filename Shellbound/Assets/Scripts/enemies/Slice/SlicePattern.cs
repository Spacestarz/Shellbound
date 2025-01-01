using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SlicePattern : MonoBehaviour
{
    Fire fire;

    [HideInInspector] public int totalSliced = 0;
    [HideInInspector] public int sliceAmount = 3;

    public List<SliceTarget> arrows;
    public List<SliceTarget> possibleArrows;
    public List<SliceTarget> outroArrows;

    SliceTarget currentArrow;
    [HideInInspector] public SliceTarget spawnedArrow;

    SliceableObject parentSlice;

    AudioSource audioSource;
    public AudioClip sliceStart;
    public AudioClip sliceFinish;

    public SliceAnimate sliceAnimation;
    public SliceAnimate spawnedSliceAnimation;

    [HideInInspector] public bool outroSlice;

    public bool hasGoneUp;
    public bool hasGoneDown;
    public bool hasGoneLeft;
    public bool hasGoneRight;


    private void Awake()
    {
        fire = FindObjectOfType<Fire>();
        parentSlice = GetComponentInParent<SliceableObject>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void DestroyArrow()
    {
        if (spawnedArrow)
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

        if (!outroSlice)
        {
            if (totalSliced < sliceAmount)
            {
                int i;

                i = SetNextArrow();
                currentArrow = arrows[i];

                if (!ControlsTutorial.hasSliced)
                {
                    ControlsTutorial.instance.ShowDragMouse(currentArrow);
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
                try
                {
                    StartCoroutine(Camera.main.GetComponent<RotateCamera>().SetCameraLock(false));
                }
                catch { }

                ResetPattern();
            }
        }
        else
        {
            currentArrow = outroArrows[totalSliced % 2];

            PlayerSlice.SetTargetDirection(currentArrow.direction);
            spawnedArrow = Instantiate(currentArrow, transform);

            spawnedSliceAnimation = Instantiate(sliceAnimation, transform);
        }
    }

    public void ResetPattern()
    {
        hasGoneUp = false;
        hasGoneDown = false;
        hasGoneLeft = false;
        hasGoneRight = false;

        totalSliced = 0;
    }

    public void FailPattern()
    {
        if (spawnedSliceAnimation)
        {
            Destroy(spawnedSliceAnimation.gameObject);
        }

        ResetPattern();
        fire.ReturnHarpoon();

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


    private int SetNextArrow()
    {
        List<int> availableGroups = new() { 0, 1, 2, 3 };
        List<int> availableDirs = new() { 0, 1, 2 };

        ExcludeGroups(ref availableGroups);
        int groupIndex = Random.Range(0, availableGroups.Count);
        int group = availableGroups[groupIndex];

        availableDirs = ExcludeDirs(availableDirs, group);
        int dirIndex = Random.Range(0, availableDirs.Count);
        int dir = availableDirs[dirIndex];

        if (dir == 1)
        {
            switch (group)
            {
                case 0: //Up
                    hasGoneUp = true;
                    hasGoneDown = false;
                    break;
                case 1: //Down
                    hasGoneUp = false;
                    hasGoneDown = true;
                    break;
                case 2: //Left
                    hasGoneLeft = true;
                    hasGoneRight = false;
                    break;
                case 3: //Right
                    hasGoneRight = true;
                    hasGoneLeft = false;
                    break;
            }
            return group;
        }
        else if ((group == 0 || group == 2) && dir == 0) //UpLeft
        {
            hasGoneUp = true;
            hasGoneLeft = true;

            hasGoneDown = false;
            hasGoneRight = false;

            return 4;
        }
        else if ((group == 0 && dir == 2) || group == 3 && dir == 0) //UpRight
        {
            hasGoneUp = true;
            hasGoneRight = true;

            hasGoneDown = false;
            hasGoneLeft = false;

            return 5;
        }
        else if ((group == 1 && dir == 0) || group == 2 && dir == 2) //DownLeft
        {
            hasGoneDown = true;
            hasGoneLeft = true;

            hasGoneUp = false;
            hasGoneRight = false;

            return 6;
        }
        else if ((group == 1 && dir == 2) || group == 3 && dir == 2) //DownRight
        {
            hasGoneDown = true;
            hasGoneRight = true;

            hasGoneUp = false;
            hasGoneLeft = false;

            return 7;
        }
        else
        {
            //In case something goes terribly wrong
            return Random.Range(0, 8);
        }
    }

    private void ExcludeGroups(ref List<int> availableGroups)
    {
        if (hasGoneUp)
        {
            availableGroups.Remove(0);
        }
        if (hasGoneDown)
        {
            availableGroups.Remove(1);
        }
        if (hasGoneLeft)
        {
            availableGroups.Remove(2);
        }
        if (hasGoneRight)
        {
            availableGroups.Remove(3);
        }
    }


    private List<int> ExcludeDirs(List<int> availableDirs, int chosenGroup)
    {
        if (chosenGroup < 2)
        {
            if (hasGoneLeft)
            {
                RemoveDir(ref availableDirs, 0);
            }
            if (hasGoneRight)
            {
                RemoveDir(ref availableDirs, 2);
            }
        }
        else
        {
            if (hasGoneUp)
            {
                RemoveDir(ref availableDirs, 0);
            }
            if (hasGoneDown)
            {
                RemoveDir(ref availableDirs, 2);
            }
        }

        return availableDirs;
    }


    private void RemoveDir(ref List<int> availableDirs, int i)
    {
        availableDirs.Remove(i);
    }
}
