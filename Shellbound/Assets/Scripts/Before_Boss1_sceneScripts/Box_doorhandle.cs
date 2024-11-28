
using DG.Tweening;
using System;
using UnityEngine;


public class Box_doorhandle : MonoBehaviour
{
    private Vector3 startpos;
    private Vector3 endpos;
    private float pingPongResult;
    private float duration = 1f;

    public bool stopPingPong = false;
    public bool gloveHome = false;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;                             
    }

    // Update is called once per frame
    void Update()
    {
        //maybe do transform go there and then go there instead of dotween
        //kolla på mathfpingpong

        if (stopPingPong == false)
        {
            Movement();
        }
        else
        {
            //make a do move 
            GetComponent<Collider>().enabled = false;
            transform.DOMove(startpos, duration).OnComplete(kill);
        }
    }

    private void kill()
    {
        gloveHome = true;
        Destroy(gameObject);
        
       
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
}
