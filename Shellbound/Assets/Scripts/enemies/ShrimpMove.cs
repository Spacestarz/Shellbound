using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpMove : MonoBehaviour
{
    private float startY;
    [SerializeField] float speed = 6f;
    [SerializeField] float height = 2f;
    private bool isHome;

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

        if (CrowdHandler != null )
        {
            Debug.Log("got the shrimp script");
        }
        else
        {
            Debug.Log("Where is mother");
        }
        //Debug.Log($"{gameObject.name} timeOffset: {timeOffset}");
    }

    
    void Update()
    {
        if (!CrowdHandler.cheerAudioSource.isPlaying && CrowdHandler.IsCheering && CrowdHandler.timeToReturn == true)
        {
            Debug.Log("returning " + "" + CrowdHandler.timeToReturn);
            CrowdHandler.IsCheering = false;
            ReturnToStartPos();
        }

        if (CrowdHandler.timeToReturn == true && isHome == false)
        {
            ReturnToStartPos();
          
        }
       /*
       if (timeToReturn)
        {
            ReturnToStartPos();
            Debug.Log("Returning");
        }
       */

        if (CrowdHandler.IsCheering)
        {         
            float newY = startY + Mathf.PingPong((Time.time + timeOffset) * speed, height);         
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        if (CrowdHandler.IsBooing)
        {
            //boo sound
        }
    }

    public void ReturnToStartPos()
    {       
        if (startY != transform.position.y && isHome == false)
        {
            Debug.Log("returning");
           // Debug.Log($"[ReturnToStartPos] Object: {gameObject.name}, CurrentY: {transform.position.y}, StartY: {startY}");
           // Debug.Log("Moving to startPOS" + startY);
            //transform.DOMoveY(startY, 3).onComplete.(timeToReturn=false);
            transform.DOMoveY(startY, 1).OnComplete(() => isHome = true);
            Debug.Log("is home is" + "" +isHome);
        }
    }
}
