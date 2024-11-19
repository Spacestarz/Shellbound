using System.Collections.Generic;
using UnityEngine;

public class SlicePattern : MonoBehaviour
{
    int totalSliced = 0;
    public int sliceAmount = 3;

    public List<SliceTarget> possibleArrows;
    SliceTarget currentArrow;
    SliceTarget spawnedArrow;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            DestroyArrow();
        }
    }

    public void DestroyArrow()
    {
        if (spawnedArrow != null)
        {
            Debug.Log("DestroyArrow");
            totalSliced++;

            Destroy(spawnedArrow.gameObject);
        }

        if (totalSliced < sliceAmount)
        {
            NextSliceArrow();
        }
    }

    public void NextSliceArrow()
    {
        Debug.Log("NextSliceArrow");
        int i;

        if (currentArrow == null)
        {
            Debug.Log("CurrentArrow null");
            i = Random.Range(0, possibleArrows.Count - 1);
        }
        else
        {
            Debug.Log("CurrentArrow not null");
            i = Random.Range(0, possibleArrows.Count - 2);
        }

        currentArrow = possibleArrows[i];
        
        MoveToEnd(i);
        spawnedArrow = Instantiate(currentArrow, transform);
    }

    void MoveToEnd(int i)
    {
        Debug.Log("MoveToEnd");
        SliceTarget value = possibleArrows[i];
        possibleArrows.RemoveAt(i);
        possibleArrows.Add(value);
    }

    void ResetPattern()
    {

    }
}
