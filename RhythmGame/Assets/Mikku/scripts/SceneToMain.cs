using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void change_button()
    {
        SceneManager.LoadScene("MainScene");
    }

}