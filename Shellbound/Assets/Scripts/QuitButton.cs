using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(QuitClicked);
    }

    // Update is called once per frame
    void QuitClicked()
    {
        SceneController.instance.GoToMenue();
    }
}
