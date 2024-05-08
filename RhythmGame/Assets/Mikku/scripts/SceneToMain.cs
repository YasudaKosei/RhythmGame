using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public void change_button()
    {
        LoadingScreenManager.LoadScene("MainScene");
    }
}