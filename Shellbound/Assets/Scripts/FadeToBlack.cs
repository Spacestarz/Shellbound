using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeToBlack : MonoBehaviour
{
    public Image blackScreen;
    bool fadeStarted;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !fadeStarted)
        {
            StartFade();
        }
    }
    
    
    void StartFade()
    {
        fadeStarted = true;
        blackScreen.enabled = true;
        blackScreen.DOColor(Color.black, 2.5f).OnComplete(NextScene);
    }


    void NextScene()
    {
        SceneController[] obj = FindObjectsByType<SceneController>(FindObjectsSortMode.None);
        obj[0].NextLevel();
    }
}
