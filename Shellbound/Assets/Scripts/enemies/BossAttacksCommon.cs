using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacksCommon : MonoBehaviour
{
    public bool velo = false;
    // Start is called before the first frame update
    public void BeVisible(GameObject obj)
    {


        var renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }

        var spriteRenderer = obj.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }

        var line = obj.GetComponent<MovingLine>();
        if (line != null)
        {
            line.SetVisible(true);
        }
        obj.GetComponent<Collider>().enabled = true;
    }

    public void BeInvisible(GameObject obj)
    {


        var renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        var spriteRenderer = obj.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        var line = obj.GetComponent<MovingLine>();
        if (line != null)
        {
            line.SetVisible(false);
        }
        obj.GetComponent<Collider>().enabled = false;
    }
}
