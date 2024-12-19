using System.Collections;           //DO NOT DELETE
using System.Collections.Generic;   //NEEDED TO WORK
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenueSpecials : MonoBehaviour
{
    public GameObject CreditText;
    RectTransform creditRect;
    float creditDefaultPos;

    public GameObject StartButon;
    GameObject QuitButon;
    GameObject manager;
    bool twening = false;
    bool goLeftNext = true;
    private void Awake()
    {
        StartButon = GameObject.Find("start");
        QuitButon = GameObject.Find("quit");
        manager = GameObject.Find("manager");
        Cursor.lockState = CursorLockMode.None;
        StartButon.GetComponent<Button>().onClick.AddListener(delegate { manager.GetComponent<SceneController>().NextLevel(); });
        QuitButon.GetComponent<Button>().onClick.AddListener(delegate { manager.GetComponent<SceneController>().quit(); });

        creditRect = CreditText.GetComponent<RectTransform>();
    }
    public void credits()
    {
        if (goLeftNext)//(CreditText.activeSelf == false && !twening)
        {
            goLeftNext = false;
            creditRect.DOKill();
            CreditText.SetActive(true);
            creditRect.DOAnchorPosX(440, 2).OnComplete(setfalse);
        }
        else if (!goLeftNext)//(CreditText.activeSelf == true)// && !twening)
        {
            goLeftNext = true;
            creditRect.DOKill();
            creditRect.DOAnchorPosX(752, 2).OnPlay(show).OnComplete(setfalse).OnComplete(hide);
        }
    }

    void hide()
    {
        CreditText.SetActive(false);
        setfalse();
    }
    void show()
    {
        CreditText.SetActive(true);
    }
    void setfalse()
    {
        twening = false;
    }
}
