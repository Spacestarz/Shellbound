using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdMove : MonoBehaviour
{
    private float startY;
    [SerializeField] float speed = 6f;
    [SerializeField] float height = 2f;

    public ShrimpCrowd ShrimpCrowd;
    
    
   
    void Start()
    {
        startY = transform.position.y;    
    }

    
    void Update()
    {
        if (ShrimpCrowd.IsCheering)
        {
            Debug.Log("cheer");
            float newY = startY + Mathf.PingPong(Time.time * speed, height);
            
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        if (ShrimpCrowd.IsBooing)
        {
            //boo sound
        }
    }
}
