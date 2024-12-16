
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    private GameObject tutorialUI;
    public GameObject player;
    private PlayerController playerController;
    private Vector3 startPOS; //startPos //just a me thing i will get better

    public Door_open door;

    public TextMeshProUGUI movedTEXT;

    public TextMeshProUGUI jumpedTEXT;

    public TextMeshProUGUI dashedTEXT;

    public TextMeshProUGUI slicedTEXT; //camelCaseUnlessAbbreviationsAreUsed (forExample"UI") //just a me thing want it to see very clearly that its a text

    public TextMeshProUGUI hookedText;

    private bool hasMoved = false;  //bools default to false //just a me thing
    private bool hasJumped = false;
    private bool hasDashed = false;
    private bool hasHooked;
    private bool hasSliced = false;

    // Start is called before the first frame update  //line break here somewhere

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();

        startPOS = player.transform.position;
        tutorialUI = GameObject.Find("TutorialUI");
        tutorialUI.SetActive(true);

        if (movedTEXT != null) //if(movedTEXT)
        {
            movedTEXT.text = "Move with WASD";
        }

        if (jumpedTEXT != null) 
        {
            jumpedTEXT.text = "Jump with space";
        }

        if (dashedTEXT != null)
        {
            dashedTEXT.text = "Dash with SHIFT";
        }

        if (hookedText)
        {
            hookedText.text = "Harpoon the painting!";
        }

        if (slicedTEXT != null)
        {
            slicedTEXT.text = "Slice that man's face!";
        }

    }

    // Update is called once per frame //You know what update does //its in as default i am lazy 
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !hasJumped)
        {
            hasJumped = true;

            //jumpedTEXT.text = "<s>Jump with space</s>";
            jumpedTEXT.text = StrikeThrough(jumpedTEXT.text); //This is just me wanting to style, :(

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
        if (hasJumped && hasDashed && hasMoved && hasHooked && hasSliced)
        {
            door.OpenDoor();
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

        TutorialCheckList();
    }

    public void GetSliced()
    {
        hasSliced = true;
        slicedTEXT.text = StrikeThrough(slicedTEXT.text);

        TutorialCheckList();
    }

    private string StrikeThrough(string str)
    {
        return "<s>" + str + "</s>";
    }
}
