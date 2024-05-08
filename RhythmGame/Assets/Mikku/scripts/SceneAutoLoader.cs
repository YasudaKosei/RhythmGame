using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneAutoLoader : MonoBehaviour
{
    [SerializeField]
    private float delayInSeconds = 3.0f;  // 遷移までの遅延時間（秒）

    [SerializeField]
    private string sceneToLoad = "HomeScene";  // ロードするシーンの名前

    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);  // 指定した秒数待機
        SceneManager.LoadScene(sceneToLoad);  // シーンをロード
    }
}

