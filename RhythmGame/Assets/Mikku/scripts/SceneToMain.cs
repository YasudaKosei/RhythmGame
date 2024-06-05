using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // オブジェクトがアクティブになった時に呼び出される
    void OnEnable()
    {
        SceneManager.LoadScene("MainScene");
    }
}
