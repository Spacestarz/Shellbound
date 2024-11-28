
using DG.Tweening;
using System;
using UnityEngine;


public class Box_doorhandle : MonoBehaviour
{
    /*
     * 
    TODO
    1. Make a public bool for the glove
    2. On the glove have a onDestroy 
    3, in the OnDestroy have a if check that checks if the bool is true
    4. is the bool true then it will open the door

    */
    private Vector3 startpos;
    private Vector3 endpos;
    private float pingPongResult;
    private float duration = 1f;

    public bool stopPingPong = false;
    public bool gloveHome = false;
    private Door_open DoorOpenScript;


    public bool DestroyandOPEN = false;

    public bool firstGlove = false;
    public bool secondGlove = false;
    public bool lastGlove = false;

    // Start is called before the first frame update
    void Start()
    {
        DoorOpenScript = GetComponentInParent<Door_open>();

        startpos = transform.position;                
    }

    // Update is called once per frame
    void Update()
    {
        //maybe do transform go there and then go there instead of dotween
        //kolla p� mathfpingpong

        if (stopPingPong == false)
        {
            Movement();
        }
        else if (stopPingPong == true && firstGlove == true) 
        {         
             
            GetComponent<Collider>().enabled = false;
            transform.DOMove(startpos, duration).OnComplete(kill);
        }
    }

    private void kill()
    {
        Debug.Log("Kill me");
        gloveHome = true;
        OnDestroy();  
    }

    private void Movement()
    {
        pingPongResult = Mathf.PingPong(Time.time,1);
        transform.position = new Vector3(startpos.x , startpos.y, startpos.z + pingPongResult);
    }

    private void OnTriggerEnter(Collider other)
    {    
        if (other.CompareTag("Harpoon"))
        {
            stopPingPong = true;
            Debug.Log("Auch said the glove");
        }      
        
    }

    public void OnDestroy()
    {
        Debug.Log("On deastroy");
        DestroyandOPEN = true;
        DoorOpenScript.OpenDoor();
        Destroy(gameObject);
    }
}
