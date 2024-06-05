using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreenControl : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider; // ���[�h�i�s�󋵂�\������X���C�_�[

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // "MainScene"��񓯊��Ń��[�h�J�n
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainScene");

        // ���[�h����������܂Ń��[�v
        while (!asyncLoad.isDone)
        {
            // asyncLoad.progress��0.0����0.9�܂ł̒l��Ԃ��̂ŁA1.0�Ŋ��邽�߂ɂ�0.9�Ŋ���K�v������܂�
            float progress = asyncLoad.progress / 0.9f;
            loadingSlider.value = progress; // �X���C�_�[�̒l���X�V
            yield return null;
        }
    }
}

