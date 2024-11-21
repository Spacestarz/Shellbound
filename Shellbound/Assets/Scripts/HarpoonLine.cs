using UnityEngine;

public class HarpoonLine : MonoBehaviour
{
    Harpoon harpoon;
    LineRenderer lineRenderer;
    public GameObject anchor;


    void Awake()
    {
        harpoon = GetComponentInParent<Harpoon>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (lineRenderer.enabled)
        {
            Debug.Log("Wakka");
            lineRenderer.SetPosition(0, anchor.transform.position);

            if (!harpoon.caughtObject)
            {
                Debug.Log("Dog");
                lineRenderer.SetPosition(1, transform.position);
            }
            else
            {
                Debug.Log("Kek");
                lineRenderer.SetPosition(1, harpoon.caughtObject.transform.position);
            }
        }
    }

    public void SetEnabled(bool status)
    {
        if (status)
        {
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
