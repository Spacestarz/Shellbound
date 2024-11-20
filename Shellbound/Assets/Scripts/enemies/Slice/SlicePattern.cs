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

    private void Awake()
    {
        parentHealth = GetComponentInParent<Enemi_Health>();
        fire = FindObjectOfType<Fire>();
    }

    private void Update()
    {
        if (PlayerSlice.activatedThisFrame && !spawnedArrow)
        {
            Debug.Log("Bog");
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
        Debug.Log("DestroyArrow");

        Destroy(spawnedArrow.gameObject);
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
            spawnedArrow = Instantiate(currentArrow, transform);
        }
        else
        {
            parentHealth.TakeDamage(10);
            fire.ReturnHarpoon();
            PlayerSlice.ToggleSliceMode();
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
}
