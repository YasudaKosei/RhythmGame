using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneToStoryAlpha : MonoBehaviour
{
    public void change_button()
    {
        SceneManager.LoadScene("StoryScene");
    }
}