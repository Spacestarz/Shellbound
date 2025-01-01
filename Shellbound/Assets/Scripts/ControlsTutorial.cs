using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System;

public class ControlsTutorial : MonoBehaviour
{
    public static ControlsTutorial instance;

    [SerializeField] GameObject[] buttons;
    /*
        0 - move
        1 - Jump
        2 - Shoot
        3 - Dash
    */


    void Start()
    {
        instance = this; 

        MakeAllButtonsTransparent();

        if (!IntroManager.isRunning)
        {
            StartTutorial();
        }
    }


    void MakeAllButtonsTransparent()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            Color goalColor = new(1, 1, 1, 0);
            buttons[i].GetComponentInChildren<Image>().color = goalColor;
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().color = goalColor;

            try { buttons[i].GetComponentInChildren<Animator>().enabled = false; }
            catch { }
        }
    }


    public void StartTutorial()
    {
        ButtonFadeIn(buttons[0]);
    }


    void ButtonFadeIn(GameObject go)
    {
        go.GetComponentInChildren<Image>().DOFade(1, 0.4f);
        go.GetComponentInChildren<TextMeshProUGUI>().DOFade(1, 0.8f);
        go.GetComponentInChildren<Animator>().enabled = true;

        StartCoroutine(InvokeMethod(ButtonFadeOut, go, 4.4f));
    }


    void ButtonFadeOut(GameObject go)
    {
        go.GetComponentInChildren<Image>().DOFade(0, 0.8f);
        go.GetComponentInChildren<TextMeshProUGUI>().DOFade(0, 0.4f);

        try { go.GetComponentInChildren<Animator>().enabled = false; }
        catch { }
    }


    IEnumerator InvokeMethod(Action<GameObject> method, GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        method(go);
        yield break;
    }
}
