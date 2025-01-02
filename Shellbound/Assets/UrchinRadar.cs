using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UrchinRadar : MonoBehaviour
{
    public static UrchinRadar instance;

    List<GameObject> urchinBlips;
    [SerializeField] GameObject urchinBlipPrefab;
    RectTransform rect;
    float rotation;

    Color fullColor;
    Color noColor;
    

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        urchinBlips = new List<GameObject>();
        instance = this;

        fullColor = new Color(1, 0, 0, 0.75f);
        noColor = new Color(1, 0, 0, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        rotation = Camera.main.GetComponent<RotateCamera>().orientation.localEulerAngles.y;
        rect.localEulerAngles = new(0, 0, rotation);

        ClearBlips();
    }

    public void NewBlip(Vector2 urchinDirection, float urchinDistance)
    {
        var newBlip = Instantiate(urchinBlipPrefab, transform);
        urchinBlips.Add(newBlip);
        newBlip.GetComponent<RectTransform>().anchoredPosition = urchinDirection * 100;

        newBlip.GetComponent<Image>().color = Color.Lerp(fullColor, noColor, urchinDistance / 6);
    }

    public void ClearBlips()
    {
        foreach (GameObject go in urchinBlips)
        {
            Destroy(go);
        }

        urchinBlips.Clear();
    }

    public void UpdateUrchinBlip(int i, List<Vector2> urchinPositions, Vector2 center)
    {
        Vector2 urchinDistance = (urchinPositions[i] - center);
        Vector2 urchinDirection = urchinDistance.normalized;

        for(int j = 0; j < urchinPositions.Count; j++)
        {
            NewBlip(urchinDirection, urchinDistance.magnitude);
        }
    }
}
