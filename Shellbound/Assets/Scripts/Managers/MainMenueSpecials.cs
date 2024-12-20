using System.Collections;           //DO NOT DELETE
using System.Collections.Generic;   //NEEDED TO WORK
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MainMenueSpecials : MonoBehaviour
{
    public GameObject CreditText;
    public GameObject setings;
    public GameObject score;
    RectTransform creditRect;
    RectTransform settingsRect;
    RectTransform scoreRect;
    public RectTransform logoRect;
    public RectTransform menueRect;
    public GameObject presText;
    //public GameObject logo;
    //public GameObject menue;
    float creditDefaultPos;

    public GameObject StartButon;
    GameObject QuitButon;
    GameObject manager;
    //bool twening = false;
    bool creditsgoLeftNext = true;
    bool setingsgoRigthNext = true;
    bool scoreGoLefthNext = true;
    bool clicked = true;
    bool leftIsOcupied = false;

    AudioSource sorce;
    public AudioClip start;
    public AudioClip end;
    public AudioClip roar;


    private void Awake()
    {
        StartButon = GameObject.Find("start");
        QuitButon = GameObject.Find("quit");
        manager = GameObject.Find("manager");
        Cursor.lockState = CursorLockMode.None;
        StartButon.GetComponent<Button>().onClick.AddListener(delegate { manager.GetComponent<SceneController>().NextLevel(); });
        QuitButon.GetComponent<Button>().onClick.AddListener(delegate { manager.GetComponent<SceneController>().quit(); });

        creditRect = CreditText.GetComponent<RectTransform>();
        settingsRect = setings.GetComponent<RectTransform>();
        scoreRect = score.GetComponent<RectTransform>();
        //logoRect = logo.GetComponent<RectTransform>();
        //menueRect = menue.GetComponent<RectTransform>();

        sorce = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (clicked && Input.anyKey)
        {
            clicked = false;
            PresedEnyKey();
        }
    }
    public void credits()
    {
        if (creditsgoLeftNext)//(CreditText.activeSelf == false && !twening)
        {
            creditsgoLeftNext = false;
            scoreGoLefthNext = true;
            sorce.PlayOneShot(start);
            scoreRect.DOKill();
            scoreRect.DOAnchorPosX(752, 2).OnPlay(() => { show(score); }).OnComplete(() => { hide(score); });
            creditRect.DOKill();
            creditRect.DOAnchorPosX(375, 2).OnPlay(()=> { show(CreditText); });
        }
        else if (!creditsgoLeftNext)//(CreditText.activeSelf == true)// && !twening)
        {
            creditsgoLeftNext = true;
            sorce.PlayOneShot(end);
            creditRect.DOKill();
            creditRect.DOAnchorPosX(752, 2).OnPlay(() => { show(CreditText); }).OnComplete(() => { hide(CreditText); });
        }
    }
    public void Score()
    {
        if (scoreGoLefthNext)//(CreditText.activeSelf == false && !twening)
        {
            scoreGoLefthNext = false;
            creditsgoLeftNext = true;
            sorce.PlayOneShot(start);
            creditRect.DOKill();
            creditRect.DOAnchorPosX(752, 2).OnPlay(() => { show(CreditText); }).OnComplete(() => { hide(CreditText); });
            scoreRect.DOKill();
            scoreRect.DOAnchorPosX(375, 2).OnPlay(() => { show(score); });
        }
        else if (!scoreGoLefthNext)//(CreditText.activeSelf == true)// && !twening)
        {
            scoreGoLefthNext = true;
            sorce.PlayOneShot(end);
            scoreRect.DOKill();
            scoreRect.DOAnchorPosX(752, 2).OnPlay(() => { show(score); }).OnComplete(() => { hide(score); });
        }
    }
    public void settings()
    {
        if (setingsgoRigthNext)//(CreditText.activeSelf == false && !twening)
        {
            setingsgoRigthNext = false;
            sorce.PlayOneShot(start);
            settingsRect.DOKill();
            show(setings);
            settingsRect.DOAnchorPosX(-220, 2);
        }
        else if (!setingsgoRigthNext)//(CreditText.activeSelf == true)// && !twening)
        {
            setingsgoRigthNext = true;
            sorce.PlayOneShot(end);
            settingsRect.DOKill();
            settingsRect.DOAnchorPosX(-600, 2).OnPlay(() => { show(setings); }).OnComplete(() => { hide(setings); });
        }
    }
    void PresedEnyKey()
    {
        sorce.PlayOneShot(roar,0.5f);
        presText.GetComponent<TextMeshProUGUI>().DOFade(0, 2).OnComplete(() => { hide(presText); });
        logoRect.DOAnchorPosY(-75, 2);
        menueRect.DOAnchorPosY(0, 2);
    }

    void hide(GameObject screen)
    {
        screen.SetActive(false);
    }
    void show(GameObject screen)
    {
        screen.SetActive(true);
    }
}
