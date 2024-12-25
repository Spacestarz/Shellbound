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
            fadeStarted = true;
            blackScreen.enabled = true;
            blackScreen.DOColor(Color.black, 1.5f).OnComplete(NextScene);
        }
    }


    void NextScene()
    {
       sceneController.NextLevel();
    }
}
