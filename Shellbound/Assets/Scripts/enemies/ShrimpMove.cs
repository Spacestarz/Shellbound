using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpMove : MonoBehaviour
{
    private float startY;
    [SerializeField] float speed = 6f;
    [SerializeField] float height = 2f;

    public ShrimpCrowd CrowdHandler;
    
    
   
    void Start()
    {
        startY = transform.position.y;    
    }

    
    void Update()
    {      

        if (CrowdHandler.IsCheering)
        {
            Debug.Log("cheer");
            float newY = startY + Mathf.PingPong(Time.time * speed, height);
            
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        if (CrowdHandler.IsBooing)
        {
            //boo sound
        }
    }
}
