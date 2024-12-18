using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    private int minSliderSensitivity = 10;
    private int maxSliderSensitivity = 60;

    private int sliderSensitivity = 30;
    public static int sensitivity = 300;

    public static SettingsManager instance;

    public Slider slider;
    public TextMeshProUGUI text;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        slider.minValue = minSliderSensitivity;
        slider.maxValue = maxSliderSensitivity;

        sliderSensitivity = sensitivity / 10;
        slider.value = sliderSensitivity;
    }

    public void SetSensitivity()
    {
        sliderSensitivity = Mathf.FloorToInt(slider.value);
        sensitivity = sliderSensitivity * 10;
        text.text = sensitivity.ToString();
    }
}
