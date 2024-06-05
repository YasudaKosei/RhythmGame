using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreenControl : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider; // ロード進行状況を表示するスライダー

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // "MainScene"を非同期でロード開始
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainScene");

        // ロードが完了するまでループ
        while (!asyncLoad.isDone)
        {
            // asyncLoad.progressは0.0から0.9までの値を返すので、1.0で割るためには0.9で割る必要があります
            float progress = asyncLoad.progress / 0.9f;
            loadingSlider.value = progress; // スライダーの値を更新
            yield return null;
        }
    }
}

