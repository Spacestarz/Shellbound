using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpMove : MonoBehaviour
{
    private float startY;
    [SerializeField] float speed = 6f;
    [SerializeField] float height = 2f;
    private bool returnHasBeenInvoked;

    [Header("Delays between")]
    float minOffset = 0;
    float maxOffset = 0.700f;
    /*
    //TODO 
    //make a random chance that they go up or down
    one vector a bit higher or lower
    
    */
    private float timeOffset;
    private ShrimpCrowd CrowdHandler;
    // public bool timeToReturn;

    private float sinSpeed;
    private int direction;

    private bool hasReturned = true;
         
    void Start()
    {
        startY = transform.localPosition.y;
        timeOffset = Random.Range(minOffset, maxOffset);
        CrowdHandler = FindAnyObjectByType<ShrimpCrowd>();

        sinSpeed = Random.Range(2.0f, 5.0f);
        direction = (int)Mathf.Sign(Random.Range(-5, 5));
        if (direction == 0)
        {
            direction = Random.Range(1, 3);
            if (direction == 2)
            {
                direction = -1;
            }
        }
        //Debug.Log($"{gameObject.name} timeOffset: {timeOffset}");
    }

    
    void Update()
    {
        if (CrowdHandler.IsCheering)
        {
            float newY = startY + Mathf.PingPong((Time.time + timeOffset) * speed, height) * direction;
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);

            if(!returnHasBeenInvoked)
            {
                hasReturned = false;
                returnHasBeenInvoked = true;
                Invoke(nameof(ReturnToStartPos), CrowdHandler.cheerAudioSource.clip.length);
            }
        }
        else if(hasReturned)
        {
            float newY = startY + Mathf.Sin(Time.time * sinSpeed) * 0.2f * direction;
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        }

        if (CrowdHandler.IsBooing)
        {
            //boo sound
        }
    }

    public void ReturnToStartPos()
    {
        transform.DOLocalMoveY(startY, 0.5f).OnComplete(SetReturnComplete);
        returnHasBeenInvoked = false;
    }

    void SetReturnComplete()
    {
        hasReturned = true;
    }
}
