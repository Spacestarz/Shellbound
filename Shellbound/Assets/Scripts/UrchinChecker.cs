using System.Collections.Generic;
using UnityEngine;

public class UrchinChecker : MonoBehaviour
{
    Vector2 center;

    LayerMask enemyMask;

    [HideInInspector] public List<GameObject> urchins;
    [HideInInspector] public List<Vector2> urchinPositions;


    private void Start()
    {
        enemyMask = LayerMask.GetMask("Enemy");
        urchins = new List<GameObject>();
        urchinPositions = new List<Vector2>();
    }

    private void Update()
    {
        center = new Vector2(transform.position.x, transform.position.z);

        UpdateUrchinPos();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<urchin>())
        {
            Vector2 urchinPos = new(other.transform.position.x, other.transform.position.z);

            urchins.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<urchin>())
        {
            int index = urchins.IndexOf(other.gameObject);
            urchins.Remove(other.gameObject);
        }
    }

    void UpdateUrchinPos()
    {
        urchinPositions.Clear();

        UrchinRadar.instance.ClearBlips();
        for (int i = 0; i < urchins.Count; i++)
        {
            if(urchins[i] != null)
            {
                Vector2 urchinPos = new(urchins[i].transform.position.x, urchins[i].transform.position.z);
                urchinPositions.Add(urchinPos);
                UrchinRadar.instance.UpdateUrchinBlip(i, urchinPositions, center);
            }
        }
    }
}
