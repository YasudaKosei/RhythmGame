using UnityEngine;
using UnityEngine.UI;

public class BrightnessSlider : MonoBehaviour
{
    public Slider brightnessSlider;

    void Start()
    {
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
    }

    void SetBrightness(float value)
    {
        // ���邳�̒����i�����ł͊ȈՓI�ɔw�i�F��ύX�j
        RenderSettings.ambientLight = Color.white * value;
    }
}
