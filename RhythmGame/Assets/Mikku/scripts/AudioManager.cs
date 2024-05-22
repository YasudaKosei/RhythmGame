using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioMixer audioMixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // ゲーム起動時に音量設定を適用
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
            float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
            float seVolume = PlayerPrefs.GetFloat("SEVolume", 0.75f);

            SetMasterVolume(masterVolume);
            SetBGMVolume(bgmVolume);
            SetSEVolume(seVolume);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMasterVolume(float value)
    {
        if (value == 0)
        {
            audioMixer.SetFloat("MasterVolume", -80); // ミュート
        }
        else
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
        }
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void SetBGMVolume(float value)
    {
        if (value == 0)
        {
            audioMixer.SetFloat("BGMVolume", -80); // ミュート
        }
        else
        {
            audioMixer.SetFloat("BGMVolume", Mathf.Log10(value) * 20);
        }
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    public void SetSEVolume(float value)
    {
        if (value == 0)
        {
            audioMixer.SetFloat("SEVolume", -80); // ミュート
        }
        else
        {
            audioMixer.SetFloat("SEVolume", Mathf.Log10(value) * 20);
        }
        PlayerPrefs.SetFloat("SEVolume", value);
    }
}
