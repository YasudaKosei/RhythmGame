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
        // 明るさの調整（ここでは簡易的に背景色を変更）
        RenderSettings.ambientLight = Color.white * value;
    }
}
