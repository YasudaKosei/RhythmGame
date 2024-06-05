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
            GameObject obj = Instantiate(objectToPool, content.transform, false);
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
        GameObject newObj = Instantiate(objectToPool, content.transform, false);
        newObj.SetActive(false);
        pooledObjects.Add(newObj);
        return newObj;
    }

    bool IsInView(GameObject obj)
    {
        RectTransform scrollViewRect = content.parent.parent as RectTransform;  // content �̑c���� ScrollView ��z��
        Vector2 screenPoint = obj.transform.position;  // �I�u�W�F�N�g�̃X�N���[�����W
        Vector2 localPoint = scrollViewRect.InverseTransformPoint(screenPoint);
        bool isInView = scrollViewRect.rect.Contains(localPoint);

        // �������ʒu�Ńf�o�b�O�����o��
        Debug.Log($"ScrollView Rect: {scrollViewRect.rect}, Object Position: {localPoint}, IsInView: {isInView}");

        return isInView;
    }

    bool IsOutOfView(GameObject obj)
    {
        RectTransform scrollViewRect = content.parent.parent as RectTransform;
        Vector2 screenPoint = obj.transform.position;

        // ScrollView��RectTransform���ŃI�u�W�F�N�g�̈ʒu���`�F�b�N
        Vector2 localPoint = scrollViewRect.InverseTransformPoint(screenPoint);
        bool outOfHorizontalBounds = localPoint.x < scrollViewRect.rect.xMin || localPoint.x > scrollViewRect.rect.xMax;
        bool outOfVerticalBounds = localPoint.y < scrollViewRect.rect.yMin || localPoint.y > scrollViewRect.rect.yMax;

        return outOfHorizontalBounds || outOfVerticalBounds;
    }

    public void Update()
    {
        foreach (GameObject obj in pooledObjects)
        {
            bool isInView = IsInView(obj);
            bool isOutOfView = IsOutOfView(obj);

            Debug.Log($"Object {obj.name} is in view: {isInView}, is out of view: {isOutOfView}");

            if (obj.activeInHierarchy && isOutOfView)
            {
                // �A�C�e�����r���[�͈͊O�Ɉړ��������A�N�e�B�u��
                obj.SetActive(false);
                Debug.Log($"Deactivating object {obj.name} because it's out of view.");
            }

            if (!obj.activeInHierarchy && isInView)
            {
                // �A�C�e�����r���[�͈͓��ɓ�������A�N�e�B�u��
                obj.SetActive(true);
                Debug.Log($"Activating object {obj.name} because it's in view.");
            }

            if (obj.activeInHierarchy && isOutOfView)
            {
                // �A�C�e�������T�C�N�����Ĕ��Α��Ɉړ�
                RecycleObject(obj);
            }
        }
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
