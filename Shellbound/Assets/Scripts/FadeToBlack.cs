using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeToBlack : MonoBehaviour
{
    public Image blackScreen;
    public bool fadeStarted;

    SceneController sceneController;

    private void Awake()
    {
        fadeStarted = false;
        sceneController = FindAnyObjectByType<SceneController>();
        blackScreen.color = new Color(0, 0, 0, 0);
    }

    public void StartFade()
    {
        float duration = FindAnyObjectByType<MainMenuMusic>().startSound.length;
        fadeStarted = true;
        blackScreen.enabled = true;
        blackScreen.DOColor(Color.black, duration);//.OnComplete(NextScene);
        Invoke(nameof(NextScene), duration);
    }


    public void NextScene()
    {
       sceneController.LoadScene("MainScene 1");
    }
}
