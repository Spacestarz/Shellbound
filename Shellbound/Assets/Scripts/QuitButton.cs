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
        yield return new WaitForSeconds(0.1f);
        if(SceneManager.GetActiveScene().name.Contains("MainMenu"))
        {
            button.onClick.AddListener(TurnOffClicked);
        }


        if (SceneManager.GetActiveScene().name.Contains("Victory") && GameObject.Find("enteryourname"))
        {
            Destroy(gameObject);
        }
    }

    public void QuitClicked()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (GameObject.Find("MusicManager"))
        {
            Destroy(GameObject.Find("MusicManager"));
        }
        SceneManager.LoadScene(0);

    }

    public void TurnOffClicked()
    {
        SceneController.instance.quit();
    }
}
