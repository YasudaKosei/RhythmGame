using UnityEngine;
using UnityEngine.EventSystems;

public class DiagonalScrollView : MonoBehaviour, IDragHandler
{
    public RectTransform content;
    public float scrollSpeed = 0.1f;  // スクロールの速度を調整するためのパラメータ

    public void OnDrag(PointerEventData eventData)
    {
        // 斜め方向（例: 右上）のベクトルを定義
        Vector2 diagonalDirection = new Vector2(1, 1).normalized;

        // ドラッグイベントからの移動量を斜め方向の成分のみに制限
        float movementMagnitude = Vector2.Dot(eventData.delta, diagonalDirection) * scrollSpeed;

        // 斜め方向にのみ移動を適用
        content.anchoredPosition += diagonalDirection * movementMagnitude;
    }
}
