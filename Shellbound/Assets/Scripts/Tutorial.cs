using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Tutorial : MonoBehaviour
{
    private GameObject tutorialUI;
    public GameObject player;
    public PlayerController playerController;
    private Vector3 startPOS;

    public TextMeshProUGUI movedTEXT;

    public TextMeshProUGUI jumpedTEXT;

    public TextMeshProUGUI dashedTEXT;

    public TextMeshProUGUI slicedTEXT;

    private bool hasmoved = false;
    private bool hasjumped = false;
    private bool hasDashed = false;
    private bool hasSliced = false;
    // Start is called before the first frame update
    void Start()
    {  
        player.transform.position = startPOS;
        tutorialUI = GameObject.Find("TutorialUI");
        tutorialUI.SetActive(true);   
        
        if (movedTEXT != null )
        {
            movedTEXT.text = "Move with WASD";
        }

        if ( jumpedTEXT != null )
        {
            jumpedTEXT.text = "Jump with space";
        }

        if( dashedTEXT != null)
        {
            dashedTEXT.text = "Dash with SHIFT";
        }

        if ( slicedTEXT != null )
        {

        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !hasjumped)
        {
            hasjumped = true;
            jumpedTEXT.text = "<s>Jump with space</s>";
            tutorialCheckList();
        }

        if (transform.position != startPOS && !hasmoved)
        {         
            hasmoved = true;
            movedTEXT.text = "<s>Move with WASD</s>";
            tutorialCheckList();
        }

        if (playerController.dashing == true && !hasDashed)
        {
            hasDashed = true;
            dashedTEXT.text = "<s>Dash with SHIFT</s>";
            tutorialCheckList();
        }

        //insert if player has hooked

    }

    public void tutorialCheckList()
    {
        tutorialUI.SetActive(true);
        if (hasjumped && hasDashed && hasmoved)
        {
            Debug.Log("Time to fight");
        }   
    }  
}
