using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // �I�u�W�F�N�g���A�N�e�B�u�ɂȂ������ɌĂяo�����
    void OnEnable()
    {
        SceneManager.LoadScene("MainScene");
    }
}
