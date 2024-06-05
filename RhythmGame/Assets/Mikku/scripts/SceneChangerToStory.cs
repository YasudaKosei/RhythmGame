using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerToStory : MonoBehaviour
{
    public void change_button()
    {
        SceneManager.LoadScene("StoryScene");
    }

}