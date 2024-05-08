using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class LoadingAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText;
    [SerializeField] private float bounceHeight = 10f;
    [SerializeField] private float bounceDuration = 0.5f;
    [SerializeField] private float delayBetweenCharacters = 0.2f;

    private Coroutine animationCoroutine; // コルーチンを保持する変数

    void Start()
    {
        StartCoroutine(AnimateTextLoop());
    }

    private IEnumerator AnimateTextLoop()
    {
        while (true) // 無限ループで処理を続ける
        {
            tmpText.ForceMeshUpdate(); // メッシュを更新して最新の情報を取得

            Vector3[] originalVertices = tmpText.mesh.vertices; // オリジナルの頂点位置を保持
            int vertexIndex;
            TMP_CharacterInfo charInfo;

            tmpText.maxVisibleCharacters = 0;

            for (int i = 0; i < tmpText.text.Length; i++)
            {
                tmpText.maxVisibleCharacters = i + 1;
                tmpText.ForceMeshUpdate();

                charInfo = tmpText.textInfo.characterInfo[i];

                if (charInfo.isVisible)
                {
                    vertexIndex = charInfo.vertexIndex;
                    Vector3 offset = Vector3.up * bounceHeight;

                    // DOTweenを使用して頂点を動かす
                    Sequence sequence = DOTween.Sequence();
                    sequence.Append(DOTween.To(() => originalVertices[vertexIndex], x => UpdateVertex(tmpText, vertexIndex, x), originalVertices[vertexIndex] + offset, bounceDuration));
                    sequence.Append(DOTween.To(() => originalVertices[vertexIndex] + offset, x => UpdateVertex(tmpText, vertexIndex, x), originalVertices[vertexIndex], bounceDuration));

                    yield return new WaitForSeconds(delayBetweenCharacters);
                }
            }
            yield return new WaitForSeconds(delayBetweenCharacters * 5); // ループの間隔を設定するための待機時間
        }
    }

    void UpdateVertex(TMP_Text text, int vertexIndex, Vector3 position)
    {
        Vector3[] vertices = text.mesh.vertices;
        vertices[vertexIndex] = position;
        text.mesh.vertices = vertices;
        text.canvasRenderer.SetMesh(text.mesh);
    }
    void OnDisable()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine); // コルーチンを停止
        }
        DOTween.KillAll(); // すべてのDOTweenアニメーションを停止
    }
}