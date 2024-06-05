using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider seSlider;

    void Start()
    {
        // AudioManagerのインスタンスが存在することを確認
        if (AudioManager.Instance != null)
        {
            // スライダーの初期値をPlayerPrefsから取得
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
            bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
            seSlider.value = PlayerPrefs.GetFloat("SEVolume", 0.75f);

            // スライダーのリスナーを設定
            masterSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
            bgmSlider.onValueChanged.AddListener(AudioManager.Instance.SetBGMVolume);
            seSlider.onValueChanged.AddListener(AudioManager.Instance.SetSEVolume);
        }
    }
}