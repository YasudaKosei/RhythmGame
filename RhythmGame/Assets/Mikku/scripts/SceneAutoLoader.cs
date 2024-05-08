using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneAutoLoader : MonoBehaviour
{
    [SerializeField]
    private float delayInSeconds = 3.0f;  // �J�ڂ܂ł̒x�����ԁi�b�j

    [SerializeField]
    private string sceneToLoad = "HomeScene";  // ���[�h����V�[���̖��O

    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);  // �w�肵���b���ҋ@
        SceneManager.LoadScene(sceneToLoad);  // �V�[�������[�h
    }
}

