using UnityEngine;
using UnityEngine.UI;  // UnityEngine.UI���g�p����ꍇ

public class TextColorPulse : MonoBehaviour
{
    public Color startColor;
    public Color endColor;
    public float pulseSpeed;

    private Text textComponent;  // UnityEngine.UI��Text���g�p����ꍇ

    void Start()
    {
        textComponent = GetComponent<Text>();  // UnityEngine.UI��Text���擾
    }

    void Update()
    {
        float t = Mathf.Sin(Time.time * pulseSpeed) * 0.5f + 0.5f;  // �����g���g�p����0����1�̒l�𐶐�
        textComponent.color = Color.Lerp(startColor, endColor, t);  // Lerp�֐��ŐF����
    }
}
