using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpMove : MonoBehaviour
{
    private float startY;
    [SerializeField] float speed = 6f;
    [SerializeField] float height = 2f;

    [Header("Delays between")]
    float minOffset = 0;
    float maxOffset = 0.700f;
    /*
    //TODO 
    //make a random chance that they go up or down
    one vector a bit higher or lower
    
    */
    private float timeOffset;
    public ShrimpCrowd CrowdHandler;
         
    void Start()
    {
        startY = transform.position.y;
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
        }

        if (CrowdHandler.IsBooing)
        {
            //boo sound
        }
    }
}
