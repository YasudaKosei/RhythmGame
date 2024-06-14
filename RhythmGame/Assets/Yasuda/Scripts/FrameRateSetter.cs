using UnityEngine;

public class FrameRateSetter : MonoBehaviour
{
    [SerializeField] private int targetFrameRate = 60;

    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}
