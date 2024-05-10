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
        // プールから非アクティブなオブジェクトを探す
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        // 必要に応じて新しいオブジェクトをプールに追加
        GameObject newObj = Instantiate(objectToPool, content.transform, false);
        newObj.SetActive(false);
        pooledObjects.Add(newObj);
        return newObj;
    }

    bool IsInView(GameObject obj)
    {
        RectTransform scrollViewRect = content.parent.parent as RectTransform;  // content の祖父が ScrollView を想定
        Vector2 screenPoint = obj.transform.position;  // オブジェクトのスクリーン座標
        Vector2 localPoint = scrollViewRect.InverseTransformPoint(screenPoint);
        bool isInView = scrollViewRect.rect.Contains(localPoint);

        // 正しい位置でデバッグ情報を出力
        Debug.Log($"ScrollView Rect: {scrollViewRect.rect}, Object Position: {localPoint}, IsInView: {isInView}");

        return isInView;
    }

    bool IsOutOfView(GameObject obj)
    {
        RectTransform scrollViewRect = content.parent.parent as RectTransform;
        Vector2 screenPoint = obj.transform.position;

        // ScrollViewのRectTransform内でオブジェクトの位置をチェック
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
                // アイテムがビュー範囲外に移動したら非アクティブ化
                obj.SetActive(false);
                Debug.Log($"Deactivating object {obj.name} because it's out of view.");
            }

            if (!obj.activeInHierarchy && isInView)
            {
                // アイテムがビュー範囲内に入ったらアクティブ化
                obj.SetActive(true);
                Debug.Log($"Activating object {obj.name} because it's in view.");
            }

            if (obj.activeInHierarchy && isOutOfView)
            {
                // アイテムをリサイクルして反対側に移動
                RecycleObject(obj);
            }
        }
    }

    void RecycleObject(GameObject obj)
    {
        // オブジェクトをリサイクルして新しい位置に配置
        obj.transform.position = CalculateNewPosition();
        UpdateObjectData(obj);
    }

    Vector3 CalculateNewPosition()
    {
        // ここでは例として、contentの右端に位置を設定するロジックを示します。
        // 実際の値は、スクロールビューのサイズやオブジェクトのサイズに基づいて調整する必要があります。
        float newX = content.rect.width;
        float newY = content.rect.height; // 例として、高さの調整も可能です。
        return new Vector3(newX, newY, 0);
    }

    void UpdateObjectData(GameObject obj)
    {
        // データを更新する具体的なロジックをここに記述します。
        // 例えば、テキストコンポーネントを持つオブジェクトの場合:
        /*var textComponent = obj.GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.text = "New Data"; // 新しいテキストデータを設定
        }*/
    }
}
