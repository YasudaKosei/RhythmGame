using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;
    public Slider progressBar;

    void Awake()
    {
        Debug.Log("Awake called in LoadingScreenManager");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Instance set in LoadingScreenManager");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void LoadScene(string sceneName)
    {
        if (Instance != null)
        {
            Debug.Log("Instance is available, starting scene load.");
            Instance.StartCoroutine(Instance.WaitForInitialization(sceneName));
        }
        else
        {
            Debug.LogError("LoadingScreenManager instance is not set.");
        }
    }

    private IEnumerator LoadGameSceneAsync(string sceneName)
    {
        AsyncOperation gameLevelLoad = SceneManager.LoadSceneAsync(sceneName);
        gameLevelLoad.allowSceneActivation = false;

        while (!gameLevelLoad.isDone)
        {
            float progress = Mathf.Clamp01(gameLevelLoad.progress / 0.9f);
            // �����Ői�s�󋵃o�[���X�V����R�[�h������ꍇ
            // progressBar.value = progress;

            if (gameLevelLoad.progress >= 0.9f)
            {
                gameLevelLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    IEnumerator WaitForInitialization(string sceneName)
    {
        // �V�[���̏���������������܂ŏ����҂�
        yield return new WaitForSeconds(0.5f);  // 0.5�b�̒x����݂���

        // �񓯊��ŃV�[�����[�h���J�n
        StartCoroutine(LoadGameSceneAsync(sceneName));
    }
}

