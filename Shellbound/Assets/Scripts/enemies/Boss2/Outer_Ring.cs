using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outer_Ring : MonoBehaviour
{
    public bool playerPresent = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Outer spere");
            playerPresent = true;
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("EXIT Outer spere");
            playerPresent = false;
        }
    }
}
