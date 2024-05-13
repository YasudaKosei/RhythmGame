using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneToHome : MonoBehaviour
{
    public void change_button()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
