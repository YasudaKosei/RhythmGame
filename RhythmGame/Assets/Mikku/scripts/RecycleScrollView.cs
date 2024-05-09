/*using UnityEngine;

public class RecycleScrollView : MonoBehaviour
{
    public RectTransform contentPanel;
    public RectTransform[] items;  // �R���e���c�A�C�e���̔z��

    void Update()
    {
        foreach (RectTransform item in items)
        {
            // �A�C�e�����r���[�|�[�g�̍����[��ʂ�߂������`�F�b�N
            if (item.anchoredPosition.x < -contentPanel.rect.width || item.anchoredPosition.y < -contentPanel.rect.height)
            {
                // �A�C�e���̈ʒu���΂ߕ����̍ł������[�Ɉړ�
                float highestX = GetHighestItemX() + item.rect.width;
                float highestY = GetHighestItemY() + item.rect.height;
                item.anchoredPosition = new Vector2(highestX, highestY);
                Debug.Log("Recycled item: " + item.name + " to new position: " + item.anchoredPosition);
            }
        }
    }

    float GetHighestItemX()
    {
        float highestX = -Mathf.Infinity;
        foreach (RectTransform item in items)
        {
            if (item.anchoredPosition.x > highestX)
            {
                highestX = item.anchoredPosition.x;
            }
        }
        return highestX;
    }

    float GetHighestItemY()
    {
        float highestY = -Mathf.Infinity;
        foreach (RectTransform item in items)
        {
            if (item.anchoredPosition.y > highestY)
            {
                highestY = item.anchoredPosition.y;
            }
        }
        return highestY;
    }
}*/
using UnityEngine;

public class RecycleScrollView : MonoBehaviour
{
    public RectTransform contentPanel;
    public RectTransform[] items;  // �R���e���c�A�C�e���̔z��

    void Update()
    {
        foreach (RectTransform item in items)
        {
            // �A�C�e�����r���[�|�[�g�̍����[��ʂ�߂������`�F�b�N
            if (item.anchoredPosition.x < -contentPanel.rect.width || item.anchoredPosition.y < -contentPanel.rect.height)
            {
                // �A�C�e���̈ʒu���΂ߕ����̍ł������[�Ɉړ�
                float highestX = GetHighestItemX() + item.rect.width;
                float highestY = GetHighestItemY() + item.rect.height;
                item.anchoredPosition = new Vector2(highestX, highestY);
                Debug.Log("Recycled item: " + item.name + " to new position: " + item.anchoredPosition);
            }
        }
    }

    float GetHighestItemX()
    {
        float highestX = -Mathf.Infinity;
        foreach (RectTransform item in items)
        {
            if (item.anchoredPosition.x > highestX)
            {
                highestX = item.anchoredPosition.x;
            }
        }
        return highestX;
    }

    float GetHighestItemY()
    {
        float highestY = -Mathf.Infinity;
        foreach (RectTransform item in items)
        {
            if (item.anchoredPosition.y > highestY)
            {
                highestY = item.anchoredPosition.y;
            }
        }
        return highestY;
    }
}

