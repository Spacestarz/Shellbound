using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    Button button;

    IEnumerator Start()
    {
        button = GetComponent<Button>();
        if(!SceneManager.GetActiveScene().name.Contains("MainMenu"))
        {
            button.onClick.AddListener(QuitClicked);
        }
        else
        {
            button.onClick.AddListener(TurnOffClicked);
        }
        
        yield return new WaitForSeconds(0.1f);
        
        if(SceneManager.GetActiveScene().name.Contains("Victory") && GameObject.Find("enteryourname"))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void QuitClicked()
    {
        SceneController.instance.GoToMenue();
    }

    void TurnOffClicked()
    {
        SceneController.instance.quit();
    }
}
