using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneToChooseMusicAlpha : MonoBehaviour
{
    public void change_button()
    {
        SceneManager.LoadScene("ChooseMusic");
    }
}
