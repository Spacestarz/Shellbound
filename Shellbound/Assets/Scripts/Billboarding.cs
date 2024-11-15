using UnityEngine;

public class Billboarding : MonoBehaviour
{
    Vector3 cameradir; 

    // Update is called once per frame
    void Update()
    {
        cameradir = Camera.main.transform.forward;
        
        if (!gameObject.CompareTag("SliceTarget"))
        {
            cameradir.y = 0; 
        }

        //rotates the sprite to face it
        if (cameradir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(cameradir);
        }
    }
}
