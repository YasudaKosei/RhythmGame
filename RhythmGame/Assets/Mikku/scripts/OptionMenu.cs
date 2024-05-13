using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    void Start()
    {
        // ���ʂ̏����l��ݒ�
        volumeSlider.value = AudioListener.volume;

        // �𑜓x�̐ݒ�
        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        foreach (var res in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(res.ToString()));
        }
        resolutionDropdown.RefreshShownValue();

        // �C�x���g���X�i�[�̐ݒ�
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
