using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneToChooseMusic : MonoBehaviour
{
    public void change_button()
    {
        SceneManager.LoadScene("ChooseMusic");
    }

}
