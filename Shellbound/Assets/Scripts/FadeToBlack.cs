using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeToBlack : MonoBehaviour
{
    public Image blackScreen;
    bool fadeStarted;

    SceneController sceneController;

    private void Awake()
    {
        sceneController = FindAnyObjectByType<SceneController>();
        blackScreen.enabled = false;
    }

    public void StartFade()
    {
        if(!fadeStarted)
        {
            float duration = FindAnyObjectByType<MainMenuMusic>().startSound.length;
            fadeStarted = true;
            blackScreen.enabled = true;
            blackScreen.DOColor(Color.black, duration).OnComplete(NextScene);
        }
    }


    void NextScene()
    {
       sceneController.NextLevel();
    }
}
