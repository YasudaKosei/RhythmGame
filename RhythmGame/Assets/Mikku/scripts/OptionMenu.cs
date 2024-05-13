using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    void Start()
    {
        // 音量の初期値を設定
        volumeSlider.value = AudioListener.volume;

        // 解像度の設定
        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        foreach (var res in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(res.ToString()));
        }
        resolutionDropdown.RefreshShownValue();

        // イベントリスナーの設定
        volumeSlider.onValueChanged.AddListener(SetVolume);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
