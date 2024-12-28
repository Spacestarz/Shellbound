using DG.Tweening;
using UnityEngine;

public class Sushi : MonoBehaviour
{
    float startY;
    float bobSpeed = 1.25f;
    float bobHeight = 0.5f;

    bool hasMoved;

    HookableObject hookableObj;

    void Awake()
    {
        startY = transform.position.y;

        hookableObj = GetComponent<HookableObject>();
    }

    void Update()
    {
        if(!hookableObj.isCaught)
        {
            BobUpAndDown();
        }
        else if(transform.position.y !=  startY && !hasMoved)
        {
            MoveUp();
        }
    }

    void BobUpAndDown()
    {
        var pos = transform.position;
        var newY = startY + bobHeight * Mathf.Sin(Time.time * bobSpeed);
        transform.position = new Vector3(pos.x, newY, pos.z);
    }

    void MoveUp()
    {
        hasMoved = true;
        transform.DOMoveY(1, 0.2f);
    }

}
