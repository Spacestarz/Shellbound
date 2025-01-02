using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitButton : MonoBehaviour
{
    Button button;
   public DisplayScore DisplayScore;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SubmitClicked);
    }

    // Update is called once per frame
    void SubmitClicked()
    {
        DisplayScore.CheckIfValidName();
    }
}
