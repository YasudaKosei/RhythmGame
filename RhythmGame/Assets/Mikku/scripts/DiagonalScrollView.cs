using UnityEngine;
using UnityEngine.EventSystems;

public class DiagonalScrollView : MonoBehaviour, IDragHandler
{
    public RectTransform content;
    public float scrollSpeed = 0.1f;  // �X�N���[���̑��x�𒲐����邽�߂̃p�����[�^

    public void OnDrag(PointerEventData eventData)
    {
        // �΂ߕ����i��: �E��j�̃x�N�g�����`
        Vector2 diagonalDirection = new Vector2(1, 1).normalized;

        // �h���b�O�C�x���g����̈ړ��ʂ��΂ߕ����̐����݂̂ɐ���
        float movementMagnitude = Vector2.Dot(eventData.delta, diagonalDirection) * scrollSpeed;

        // �΂ߕ����ɂ݈̂ړ���K�p
        content.anchoredPosition += diagonalDirection * movementMagnitude;
    }
}
