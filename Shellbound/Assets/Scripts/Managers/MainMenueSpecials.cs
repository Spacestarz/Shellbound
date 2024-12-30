using System.Collections;           //DO NOT DELETE
using System.Collections.Generic;   //NEEDED TO WORK
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Spine.Unity;

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
    bool startButtonPressed;

    AudioSource sorce;
    public AudioClip start;
    public AudioClip end;
    public AudioClip roar;

    public SkeletonAnimation anim;


    private void Awake()
    {
        StartButon = GameObject.Find("start");
        QuitButon = GameObject.Find("quit");
        manager = GameObject.Find("manager");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //StartButon.GetComponent<Button>().onClick.AddListener(delegate { StartGame();/*manager.GetComponent<SceneController>().NextLevel();*/ });
        //QuitButon.GetComponent<Button>().onClick.AddListener(delegate { manager.GetComponent<SceneController>().quit(); });

        creditRect = CreditText.GetComponent<RectTransform>();
        settingsRect = setings.GetComponent<RectTransform>();
        scoreRect = score.GetComponent<RectTransform>();
        //logoRect = logo.GetComponent<RectTransform>();
        //menueRect = menue.GetComponent<RectTransform>();

        sorce = GetComponent<AudioSource>();

        if (SceneController.gameStarted)
        {
            clicked = true;
            SetFinalPositions();
        }
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
        MainMenuMusic mainMenuMusic = FindAnyObjectByType<MainMenuMusic>();
        float tweenLength = mainMenuMusic.introMusic.length;
        mainMenuMusic.PlayIntro();
        SceneController.gameStarted = true;

        sorce.PlayOneShot(roar,0.5f);
        presText.GetComponent<TextMeshProUGUI>().DOFade(0, tweenLength).OnComplete(() => { hide(presText); });
        logoRect.DOAnchorPosY(-75, tweenLength);
        menueRect.DOAnchorPosY(0, tweenLength);
        anim.AnimationState.SetAnimation(0, "Silly guy fade in", false);
        Invoke(nameof(SetFinalAnimation), 5.333f);
    }

    void SetFinalAnimation()
    {
        anim.AnimationState.SetAnimation(0, "Silly guy moving", true);
    }

    void hide(GameObject screen)
    {
        screen.SetActive(false);
    }
    void show(GameObject screen)
    {
        screen.SetActive(true);
    }

    public void StartGame()
    {
        if(!startButtonPressed)
        {
            startButtonPressed = true;
            FindAnyObjectByType<MainMenuMusic>().PlayStartSound();
            FindAnyObjectByType<FadeToBlack>().StartFade();
        }
    }

    void SetFinalPositions()
    {
        MainMenuMusic mainMenuMusic = FindAnyObjectByType<MainMenuMusic>();
        mainMenuMusic.PlayMainMusic();

        presText.SetActive(false);
        logoRect.anchoredPosition = new Vector3(logoRect.anchoredPosition.x, -75);
        menueRect.anchoredPosition = new Vector3(menueRect.anchoredPosition.x, 0);
        anim.AnimationState.SetAnimation(0, "Silly guy moving", false);
    }
}
