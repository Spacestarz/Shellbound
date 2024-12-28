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
         
    void Start()
    {
        startY = transform.localPosition.y;
        timeOffset = Random.Range(minOffset, maxOffset);
        CrowdHandler = FindAnyObjectByType<ShrimpCrowd>();

        //Debug.Log($"{gameObject.name} timeOffset: {timeOffset}");
    }

    
    void Update()
    {
        if (CrowdHandler.IsCheering)
        {
            float newY = startY + Mathf.PingPong((Time.time + timeOffset) * speed, height);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            if(!returnHasBeenInvoked)
            {
                returnHasBeenInvoked = true;
                Invoke(nameof(ReturnToStartPos), CrowdHandler.cheerAudioSource.clip.length);
            }
        }

        if (CrowdHandler.IsBooing)
        {
            //boo sound
        }
    }

    public void ReturnToStartPos()
    {
        transform.DOLocalMoveY(startY, 0.5f);
        returnHasBeenInvoked = false;
    }
}
