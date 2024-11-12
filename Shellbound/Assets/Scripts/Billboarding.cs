using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    Vector3 cameradir; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameradir = Camera.main.transform.forward;
        cameradir.y = 0; 

        //rotates the sprite to face it
        transform.rotation = Quaternion.LookRotation(cameradir);
    }
}
