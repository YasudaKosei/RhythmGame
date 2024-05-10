using UnityEngine;
using UnityEngine.EventSystems;

public class DiagonalScrollView : MonoBehaviour, IDragHandler
{
    public RectTransform content;
    public float scrollSpeed;  // スクロールの速度を調整するためのパラメータ
    private Vector2 diagonalDirection;  // 斜め方向の正規化されたベクトルを保持する変数

    void Start()
    {
        // 斜め方向のベクトルを定義して正規化し、メンバー変数に格納
        diagonalDirection = new Vector2(1, 1).normalized;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float movementMagnitude = Vector2.Dot(eventData.delta, diagonalDirection) * scrollSpeed;
        Vector2 newPosition = content.anchoredPosition + diagonalDirection * movementMagnitude;
        content.anchoredPosition = newPosition;
    }

}

