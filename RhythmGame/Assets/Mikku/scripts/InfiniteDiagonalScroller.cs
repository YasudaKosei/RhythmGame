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
        // プールから非アクティブなオブジェクトを探す
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        // 必要に応じて新しいオブジェクトをプールに追加
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
                // アイテムをリサイクルして反対側に移動
                RecycleObject(obj);
            }
        }
    }

    bool IsOutOfView(GameObject obj)
    {
        Vector2 viewPos = Camera.main.WorldToViewportPoint(obj.transform.position);
        // ビューポートの座標は0から1の範囲で、それを超えた場合はビュー範囲外と見なす
        return viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1;
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
