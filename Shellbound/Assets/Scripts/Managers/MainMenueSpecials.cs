using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenueSpecials : MonoBehaviour
{
    public GameObject CreditText;
    public GameObject StartButon;
    GameObject QuitButon;
    GameObject manager;
    bool twening = false;
    private void Awake()
    {
        StartButon = GameObject.Find("start");
        QuitButon = GameObject.Find("quit");
        manager = GameObject.Find("manager");
        Cursor.lockState = CursorLockMode.None;
        StartButon.GetComponent<Button>().onClick.AddListener(delegate { manager.GetComponent<SceneController>().NextLevel(); });
        QuitButon.GetComponent<Button>().onClick.AddListener(delegate { manager.GetComponent<SceneController>().quit(); });
    }
    public void credits()
    {
        if (CreditText.activeSelf == true && !twening)
        {
            twening = true;
            CreditText.transform.DOMoveX(50,2).OnComplete(hide);
           //CreditText.SetActive(false);
        }
        else if (CreditText.activeSelf == false && !twening)
        {
            twening = true;
            //CreditText.SetActive(true);
            CreditText.transform.DOMoveX(25, 2).OnPlay(show).OnComplete(setfalse);
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
