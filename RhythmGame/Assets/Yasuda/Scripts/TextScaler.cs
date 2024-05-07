using UnityEngine;
using TMPro; // TextMeshProの名前空間を追加
using System.Collections;

public class TextScaler : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // TextMeshProUGUIコンポーネントへの参照

    [SerializeField]
    private float startSize = 20f; // 開始時のテキストの大きさ

    [SerializeField]
    private float endSize = 50f; // 終了時のテキストの大きさ

    [SerializeField]
    private float duration = 2f; // アニメーションの継続時間（秒）

    // テキストを拡大する関数
    public void ScaleText()
    {
        StartCoroutine(ScaleTextCoroutine());
    }

    // コルーチンを使用してテキストを拡大する処理
    private IEnumerator ScaleTextCoroutine()
    {
        float elapsedTime = 0f;
        float newSize = startSize;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // 現在のテキストの大きさを計算
            newSize = Mathf.Lerp(startSize, endSize, elapsedTime / duration);

            // テキストの大きさを設定
            textComponent.fontSize = newSize;

            yield return null;
        }

        // アニメーションが終了したら、終了サイズにテキストの大きさを設定する
        textComponent.fontSize = endSize;
    }
}
