using UnityEngine;

public class MovingLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    
    public GameObject projectile;
    public GameObject anchor;

    Harpoon harpoon;


    void Awake()
    {
        if (gameObject.CompareTag("Harpoon"))
        {
            harpoon = projectile.GetComponent<Harpoon>();
        }

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }


    void Update()
    {
        if (lineRenderer.enabled)
        {
            lineRenderer.SetPosition(0, anchor.transform.position);

            if (gameObject.CompareTag("Harpoon"))
            {
                if (!harpoon.caughtObject)
                {
                    lineRenderer.SetPosition(1, transform.position);
                }
                else
                {
                    lineRenderer.SetPosition(1, harpoon.caughtObject.GetComponent<SliceableObject>().sliceBoard.transform.position);
                }
            }
            else
            {
                lineRenderer.SetPosition(1, transform.position);
            }
        }
    }

    public void SetVisible(bool status)
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
