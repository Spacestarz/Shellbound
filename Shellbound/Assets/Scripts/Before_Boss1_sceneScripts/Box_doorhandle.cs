
using DG.Tweening;
using System;
using UnityEngine;
using TMPro;


public class Box_doorhandle : MonoBehaviour
{
    //Pretty sure i could have made this easier with unity events? 
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

    public bool movePingPong = false;
    public TextMeshPro auchtext;

    // Start is called before the first frame update
    void Start()
    {
        DoorOpenScript = GetComponentInParent<Door_open>();
        auchtext.enabled = false;
        startpos = transform.position;                
    }

    // Update is called once per frame
    void Update()
    {
        //maybe do transform go there and then go there instead of dotween
        //kolla på mathfpingpong

        if (stopPingPong == false && movePingPong == true)
        {
            Movement();
        }
        else if (stopPingPong == true && movePingPong == true) 
        {         
             
            GetComponent<Collider>().enabled = false;
            transform.DOMove(startpos, duration).OnComplete(kill);
        }
    }

    private void kill()
    {
        Debug.Log("Kill me");
        gloveHome = true;
        Destroy(this); 
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
            auchtext.enabled = true;
            stopPingPong = true;

            Debug.Log("Auch said the glove");
        }      
        
    }

    public void OnDestroy()
    {
        Debug.Log("Destroyed opening door");
        DestroyandOPEN = true;
        DoorOpenScript.OpenDoor();
        Destroy(gameObject);
    }
}
