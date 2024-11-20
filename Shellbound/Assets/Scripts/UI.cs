using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Slider Slider;
    public HealthSystem HealthSystem;

    // Start is called before the first frame update
    void Start()
    {
        Slider.maxValue = HealthSystem.MaxHP;
        
    }

    // Update is called once per frame
    void Update()
    {
        Slider.value = HealthSystem.currentHP;

        if (Slider.value == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
