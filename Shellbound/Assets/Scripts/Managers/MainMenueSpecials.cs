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
        if (CreditText.activeSelf == true)
        {
            CreditText.transform.DOMoveX(350,2).OnComplete(hide);
           //CreditText.SetActive(false);
        }
        else if (CreditText.activeSelf == false)
        {
            //CreditText.SetActive(true);
            CreditText.transform.DOMoveX(250, 2).OnPlay(show);
        }
    }
    void hide()
    {
        CreditText.SetActive(false);
    }
    void show()
    {
        CreditText.SetActive(true);
    }

}
