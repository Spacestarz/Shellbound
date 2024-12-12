using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    private GameObject tutorialUI;
    public GameObject player;
    private PlayerController playerController;
    private Vector3 startPOS; //Pos

    public TextMeshProUGUI movedTEXT;

    public TextMeshProUGUI jumpedTEXT;

    public TextMeshProUGUI dashedTEXT;

    public TextMeshProUGUI slicedTEXT; //camelCaseUnlessAbbreviationsAreUsed (forExampleUI)

    public TextMeshProUGUI hookedText;

    private bool hasMoved = false; //bools default to false
    private bool hasJumped = false;
    private bool hasDashed = false;
    private bool hasHooked;
    private bool hasSliced = false;
    // Start is called before the first frame update  //line break
    void Start()
    {  
        playerController = player.GetComponent<PlayerController>();

        startPOS = player.transform.position;
        tutorialUI = GameObject.Find("TutorialUI");
        tutorialUI.SetActive(true);   
        
        if (movedTEXT != null ) //if(movedTEXT)
        {
            movedTEXT.text = "Move with WASD";
        }

        if ( jumpedTEXT != null ) //inconsistent spacing on all these ifs
        {
            jumpedTEXT.text = "Jump with space";
        }

        if( dashedTEXT != null)
        {
            dashedTEXT.text = "Dash with SHIFT";
        }

        if ( slicedTEXT != null )
        {
            slicedTEXT.text = "Slice that man's face";
        }

        if(hookedText)
        {
            hookedText.text = "Hook that man's face";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !hasJumped)
        {
            hasJumped = true;

            //jumpedTEXT.text = "<s>Jump with space</s>";
            jumpedTEXT.text = StrikeThrough(jumpedTEXT.text);

            TutorialCheckList();
        }

        if (CheckIfMoved() && !hasMoved)
        {         
            hasMoved = true;
            movedTEXT.text = StrikeThrough(movedTEXT.text);

            TutorialCheckList();
        }

        if (playerController.dashing == true && !hasDashed) //if (playerController.dashing...)
        {
            hasDashed = true;
            dashedTEXT.text = StrikeThrough(dashedTEXT.text);

            TutorialCheckList();
        }
    }

    public void TutorialCheckList()
    {
        tutorialUI.SetActive(true);
        if (hasJumped && hasDashed && hasMoved)
        {
            Debug.Log("Time to fight");
        }   
    }

    public bool CheckIfMoved()
    {
        if ((player.transform.position - startPOS).sqrMagnitude > 0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GetHooked()
    {
        hasHooked = true;
        hookedText.text = StrikeThrough(hookedText.text);
    }

    public void GetSliced()
    {
        hasSliced = true;
        slicedTEXT.text = StrikeThrough(slicedTEXT.text);
    }

    private string StrikeThrough(string str)
    {
        str = "<s>" + str + "</s>";
        return str;
    }

}
