using UnityEngine;
using UnityEngine.UI;  // UnityEngine.UIを使用する場合

public class TextColorPulse : MonoBehaviour
{
    public Color startColor;
    public Color endColor;
    public float pulseSpeed;

    private Text textComponent;  // UnityEngine.UIのTextを使用する場合

    void Start()
    {
        textComponent = GetComponent<Text>();  // UnityEngine.UIのTextを取得
    }

    void Update()
    {
        float t = Mathf.Sin(Time.time * pulseSpeed) * 0.5f + 0.5f;  // 正弦波を使用して0から1の値を生成
        textComponent.color = Color.Lerp(startColor, endColor, t);  // Lerp関数で色を補間
    }
}
