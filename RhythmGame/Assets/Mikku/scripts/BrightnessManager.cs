using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    public static BrightnessManager Instance { get; private set; }

    private Camera mainCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // �����l��ݒ�
            float brightness = PlayerPrefs.GetFloat("Brightness", 0.75f);
            SetBrightness(brightness);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FindAndSetupMainCamera();
    }

    private void FindAndSetupMainCamera()
    {
        mainCamera = Camera.main;

        if (mainCamera != null)
        {
            var colorFilter = mainCamera.GetComponent<ColorFilter>();
            if (colorFilter == null)
            {
                colorFilter = mainCamera.gameObject.AddComponent<ColorFilter>();
            }
            // �����̖��邳��ݒ�
            float brightness = PlayerPrefs.GetFloat("Brightness", 0.75f);
            SetBrightness(brightness);
        }
    }

    public void SetBrightness(float value)
    {
        if (mainCamera != null)
        {
            // �J�����̃J���[�t�B���^�[���g�p���Ė��邳�𒲐�
            Color color = Color.white * Mathf.Lerp(0.5f, 1.5f, value);
            mainCamera.backgroundColor = color;
            // �J�����̃J���[�t�B���^�[�̐F��ݒ�
            var colorFilter = mainCamera.GetComponent<ColorFilter>();
            if (colorFilter != null)
            {
                colorFilter.SetColor(color);
            }
        }
        PlayerPrefs.SetFloat("Brightness", value);
    }
}
