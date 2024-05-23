using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider seSlider;

    void Start()
    {
        // AudioManager�̃C���X�^���X�����݂��邱�Ƃ��m�F
        if (AudioManager.Instance != null)
        {
            // �X���C�_�[�̏����l��PlayerPrefs����擾
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
            bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
            seSlider.value = PlayerPrefs.GetFloat("SEVolume", 0.75f);

            // �X���C�_�[�̃��X�i�[��ݒ�
            masterSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
            bgmSlider.onValueChanged.AddListener(AudioManager.Instance.SetBGMVolume);
            seSlider.onValueChanged.AddListener(AudioManager.Instance.SetSEVolume);
        }
    }
}