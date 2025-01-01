using UnityEngine;

public class ControlsTutorial : MonoBehaviour
{
    [SerializeField] GameObject move;
    [SerializeField] GameObject jump;
    [SerializeField] GameObject shoot;
    [SerializeField] GameObject dash;

    void Start()
    {
        if(IntroManager.instance)
        {
            Debug.Log("Wow!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartTutorial()
    {

    }

    void ControlFadeIn()
    {

    }
}
