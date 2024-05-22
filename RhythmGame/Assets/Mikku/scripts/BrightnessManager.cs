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

            // 初期値を設定
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
            // 初期の明るさを設定
            float brightness = PlayerPrefs.GetFloat("Brightness", 0.75f);
            SetBrightness(brightness);
        }
    }

    public void SetBrightness(float value)
    {
        if (mainCamera != null)
        {
            // カメラのカラーフィルターを使用して明るさを調整
            Color color = Color.white * Mathf.Lerp(0.5f, 1.5f, value);
            mainCamera.backgroundColor = color;
            // カメラのカラーフィルターの色を設定
            var colorFilter = mainCamera.GetComponent<ColorFilter>();
            if (colorFilter != null)
            {
                colorFilter.SetColor(color);
            }
        }
        PlayerPrefs.SetFloat("Brightness", value);
    }
}
