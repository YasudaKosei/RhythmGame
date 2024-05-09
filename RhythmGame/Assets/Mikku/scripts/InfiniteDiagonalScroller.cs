using System.Collections.Generic;
using UnityEngine;

public class InfiniteDiagonalScroller : MonoBehaviour
{
    public RectTransform content;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int poolAmount;

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        // �v�[�������A�N�e�B�u�ȃI�u�W�F�N�g��T��
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        // �K�v�ɉ����ĐV�����I�u�W�F�N�g���v�[���ɒǉ�
        GameObject newObj = Instantiate(objectToPool);
        newObj.SetActive(false);
        pooledObjects.Add(newObj);
        return newObj;
    }

    public void Update()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (obj.activeInHierarchy && IsOutOfView(obj))
            {
                // �A�C�e�������T�C�N�����Ĕ��Α��Ɉړ�
                RecycleObject(obj);
            }
        }
    }

    bool IsOutOfView(GameObject obj)
    {
        Vector2 viewPos = Camera.main.WorldToViewportPoint(obj.transform.position);
        // �r���[�|�[�g�̍��W��0����1�͈̔͂ŁA����𒴂����ꍇ�̓r���[�͈͊O�ƌ��Ȃ�
        return viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1;
    }

    void RecycleObject(GameObject obj)
    {
        // �I�u�W�F�N�g�����T�C�N�����ĐV�����ʒu�ɔz�u
        obj.transform.position = CalculateNewPosition();
        UpdateObjectData(obj);
    }

    Vector3 CalculateNewPosition()
    {
        // �����ł͗�Ƃ��āAcontent�̉E�[�Ɉʒu��ݒ肷�郍�W�b�N�������܂��B
        // ���ۂ̒l�́A�X�N���[���r���[�̃T�C�Y��I�u�W�F�N�g�̃T�C�Y�Ɋ�Â��Ē�������K�v������܂��B
        float newX = content.rect.width;
        float newY = content.rect.height; // ��Ƃ��āA�����̒������\�ł��B
        return new Vector3(newX, newY, 0);
    }

    void UpdateObjectData(GameObject obj)
    {
        // �f�[�^���X�V�����̓I�ȃ��W�b�N�������ɋL�q���܂��B
        // �Ⴆ�΁A�e�L�X�g�R���|�[�l���g�����I�u�W�F�N�g�̏ꍇ:
        /*var textComponent = obj.GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.text = "New Data"; // �V�����e�L�X�g�f�[�^��ݒ�
        }*/
    }
}
