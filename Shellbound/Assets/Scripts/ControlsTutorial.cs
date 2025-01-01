using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System;

public class ControlsTutorial : MonoBehaviour
{
    public static ControlsTutorial instance;
    public static bool hasSliced;

    [SerializeField] GameObject[] buttons;
    /*
        0 - move
        1 - Jump
        2 - Shoot
        3 - Dash
    */


    IEnumerator Start()
    {
        instance = this;

        MakeAllButtonsTransparent();

        yield return new WaitForSeconds(Mathf.Epsilon);

        if (!IntroManager.isRunning)
        {
            StartTutorial();
        }

        yield break;
    }


    void MakeAllButtonsTransparent()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            Image[] images = buttons[i].GetComponentsInChildren<Image>();

            Color goalColor = new(1, 1, 1, 0);

            foreach (Image img in images)
            {
                img.color = goalColor;
            }

            buttons[i].GetComponentInChildren<TextMeshProUGUI>().color = goalColor;
        }
    }


    public void StartTutorial()
    {
        ButtonFadeIn(buttons[0]);
        StartCoroutine(InvokeMethod(ButtonFadeIn, buttons[1], 0.4f));
        StartCoroutine(InvokeMethod(ButtonFadeIn, buttons[2], 3.2f));
        StartCoroutine(InvokeMethod(ButtonFadeIn, buttons[3], 3.6f));
    }


    public void ButtonFadeIn(GameObject go)
    {
        Image[] images = go.GetComponentsInChildren<Image>();

        if (images.Length > 1)
        {
            go.GetComponentInChildren<Animator>().SetTrigger("Click");
        }

        foreach (Image img in images)
        {
            img.DOFade(1, 0.4f);
        }

        go.GetComponentInChildren<TextMeshProUGUI>().DOFade(1, 0.4f);

        StartCoroutine(InvokeMethod(ButtonFadeOut, go, 3.4f));
    }


    public void ButtonFadeOut(GameObject go)
    {
        Image[] images = go.GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            img.DOFade(0, 0.4f);
        }

        go.GetComponentInChildren<TextMeshProUGUI>().DOFade(0, 0.4f);

    }


    IEnumerator InvokeMethod(Action<GameObject> Method, GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        Method(go);
        yield break;
    }

    public void ShowDragMouse(SliceTarget currentArrow)
    {
        buttons[4].GetComponentInChildren<MouseAnimator>().Slice(currentArrow);
    }
}
