using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Trigger : MonoBehaviour
{
    private Door_open door_Open;
    
    // Start is called before the first frame update
    void Start()
    {
        door_Open = GetComponentInParent<Door_open>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
       
        door_Open.CloseDoor();
    }
}
