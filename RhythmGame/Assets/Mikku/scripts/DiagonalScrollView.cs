using UnityEngine;
using UnityEngine.EventSystems;

public class DiagonalScrollView : MonoBehaviour, IDragHandler
{
    public RectTransform content;
    public float scrollSpeed;  // �X�N���[���̑��x�𒲐����邽�߂̃p�����[�^
    private Vector2 diagonalDirection;  // �΂ߕ����̐��K�����ꂽ�x�N�g����ێ�����ϐ�

    void Start()
    {
        // �΂ߕ����̃x�N�g�����`���Đ��K�����A�����o�[�ϐ��Ɋi�[
        diagonalDirection = new Vector2(1, 1).normalized;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float movementMagnitude = Vector2.Dot(eventData.delta, diagonalDirection) * scrollSpeed;
        Vector2 newPosition = content.anchoredPosition + diagonalDirection * movementMagnitude;
        content.anchoredPosition = newPosition;
    }

}

