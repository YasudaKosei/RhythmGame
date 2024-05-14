/*using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class InfiniteScroll : UIBehaviour
{
    [SerializeField]
    private RectTransform itemPrototype;

    [SerializeField, Range(0, 30)]
    int instantateItemCount = 9;

    [SerializeField]
    private Direction direction;

    public OnItemPositionChange onUpdateItem = new OnItemPositionChange();

    [System.NonSerialized]
    public LinkedList<RectTransform> itemList = new LinkedList<RectTransform>();

    protected Vector2 diffPreFramePosition = Vector2.zero;

    protected int currentItemNo = 0;

    public enum Direction
    {
        Vertical,
        Horizontal,
        Diagonal,
    }

    // cache component

    private RectTransform _rectTransform;
    protected RectTransform rectTransform
    {
        get
        {
            if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
            return _rectTransform;
        }
    }

    private Vector2 anchoredPosition
    {
        get
        {
            switch (direction)
            {
                case Direction.Vertical:
                    return new Vector2(0, -rectTransform.anchoredPosition.y);
                case Direction.Horizontal:
                    return new Vector2(rectTransform.anchoredPosition.x, 0);
                case Direction.Diagonal:
                    return rectTransform.anchoredPosition;
                default:
                    return Vector2.zero;
            }
        }
    }

    private float _itemScale = -1;
    public float itemScale
    {
        get
        {
            if (itemPrototype != null && _itemScale == -1)
            {
                _itemScale = Mathf.Sqrt(Mathf.Pow(itemPrototype.sizeDelta.x, 2) + Mathf.Pow(itemPrototype.sizeDelta.y, 2)); // 対角線の長さを計算
            }
            return _itemScale;
        }
    }

    protected override void Start()
    {
        var controllers = GetComponents<MonoBehaviour>()
                .Where(item => item is IInfiniteScrollSetup)
                .Select(item => item as IInfiniteScrollSetup)
                .ToList();

        // create items

        var scrollRect = GetComponentInParent<ScrollRect>();
        scrollRect.horizontal = direction != Direction.Vertical;
        scrollRect.vertical = direction != Direction.Horizontal;
        scrollRect.content = rectTransform;

        itemPrototype.gameObject.SetActive(false);
        for (int i = 0; i < instantateItemCount; i++)
        {
            var item = GameObject.Instantiate(itemPrototype) as RectTransform;
            item.SetParent(transform, false);
            item.name = i.ToString();
            if (direction == Direction.Vertical)
                item.anchoredPosition = new Vector2(0, -itemScale * i);
            else if (direction == Direction.Horizontal)
                item.anchoredPosition = new Vector2(itemScale * i, 0);
            else if (direction == Direction.Diagonal)
                item.anchoredPosition = new Vector2(itemScale * i, -itemScale * i); // 対角方向に配置

            itemList.AddLast(item);

            item.gameObject.SetActive(true);

            foreach (var controller in controllers)
            {
                controller.OnUpdateItem(i, item.gameObject);
            }
        }

        foreach (var controller in controllers)
        {
            controller.OnPostSetupItems();
        }
    }

    void Update()
    {
        if (itemList.First == null)
        {
            return;
        }

        Vector2 moveDirection = new Vector2(1, -1).normalized; // -45度の方向ベクトル
        Vector2 projectedAnchoredPosition = Vector2.Dot(anchoredPosition, moveDirection) * moveDirection;
        Vector2 projectedDiffPreFramePosition = Vector2.Dot(diffPreFramePosition, moveDirection) * moveDirection;

        int safetyCounter = 0; // デバッグ用カウンター

        while (Vector2.Dot(projectedAnchoredPosition - projectedDiffPreFramePosition, moveDirection) < -itemScale * 2)
        {
            if (++safetyCounter > 1000) break; // ループの実行回数を制限

            diffPreFramePosition -= itemScale * moveDirection;

            var item = itemList.First.Value;
            itemList.RemoveFirst();
            itemList.AddLast(item);

            var pos = itemScale * (instantateItemCount + currentItemNo);
            item.anchoredPosition = pos * moveDirection; // 対角方向に配置

            onUpdateItem.Invoke(currentItemNo + instantateItemCount, item.gameObject);

            currentItemNo++;
        }

        safetyCounter = 0; // デバッグ用カウンターリセット

        while (Vector2.Dot(projectedAnchoredPosition - projectedDiffPreFramePosition, moveDirection) > 0)
        {
            if (++safetyCounter > 1000) break; // ループの実行回数を制限

            diffPreFramePosition += itemScale * moveDirection;

            var item = itemList.Last.Value;
            itemList.RemoveLast();
            itemList.AddFirst(item);

            currentItemNo--;

            var pos = itemScale * currentItemNo;
            item.anchoredPosition = pos * moveDirection; // 対角方向に配置

            onUpdateItem.Invoke(currentItemNo, item.gameObject);
        }
    }

    [System.Serializable]
    public class OnItemPositionChange : UnityEngine.Events.UnityEvent<int, GameObject> { }
}*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class InfiniteScroll : UIBehaviour
{
    [SerializeField]
    private RectTransform itemPrototype;

    [SerializeField, Range(0, 30)]
    int instantateItemCount = 9;

    [SerializeField]
    private Direction direction;

    public OnItemPositionChange onUpdateItem = new OnItemPositionChange();

    [System.NonSerialized]
    public LinkedList<RectTransform> itemList = new LinkedList<RectTransform>();

    protected Vector2 diffPreFramePosition = Vector2.zero;

    protected int currentItemNo = 0;

    public enum Direction
    {
        Vertical,
        Horizontal,
        Diagonal,
    }

    // cache component

    private RectTransform _rectTransform;
    protected RectTransform rectTransform
    {
        get
        {
            if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
            return _rectTransform;
        }
    }

    private Vector2 anchoredPosition
    {
        get
        {
            switch (direction)
            {
                case Direction.Vertical:
                    return new Vector2(0, -rectTransform.anchoredPosition.y);
                case Direction.Horizontal:
                    return new Vector2(rectTransform.anchoredPosition.x, 0);
                case Direction.Diagonal:
                    return rectTransform.anchoredPosition;
                default:
                    return Vector2.zero;
            }
        }
    }

    private float _itemScale = -1;
    public float itemScale
    {
        get
        {
            if (itemPrototype != null && _itemScale == -1)
            {
                _itemScale = Mathf.Sqrt(Mathf.Pow(itemPrototype.sizeDelta.x, 2) + Mathf.Pow(itemPrototype.sizeDelta.y, 2)); // 対角線の長さを計算
            }
            return _itemScale;
        }
    }

    protected override void Start()
    {
        var controllers = GetComponents<MonoBehaviour>()
        .Where(item => item is IInfiniteScrollSetup)
        .Select(item => item as IInfiniteScrollSetup)
        .ToList();

        // create items

        var scrollRect = GetComponentInParent<ScrollRect>();
        scrollRect.horizontal = direction != Direction.Vertical;
        scrollRect.vertical = direction != Direction.Horizontal;
        scrollRect.content = rectTransform;

        itemPrototype.gameObject.SetActive(false);

        Vector2 moveDirection = new Vector2(1, -1).normalized; // -45度の方向ベクトル

        for (int i = 0; i < instantateItemCount; i++)
        {
            var item = GameObject.Instantiate(itemPrototype) as RectTransform;
            item.SetParent(transform, false);
            item.name = i.ToString();
            if (direction == Direction.Vertical)
                item.anchoredPosition = new Vector2(0, -itemScale * i);
            else if (direction == Direction.Horizontal)
                item.anchoredPosition = new Vector2(itemScale * i, 0);
            else if (direction == Direction.Diagonal)
                item.anchoredPosition = i * itemScale * moveDirection; // 対角方向に配置

            itemList.AddLast(item);

            item.gameObject.SetActive(true);

            foreach (var controller in controllers)
            {
                controller.OnUpdateItem(i, item.gameObject);
            }
        }

        foreach (var controller in controllers)
        {
            controller.OnPostSetupItems();
        }
    }

    void Update()
    {
        if (itemList.First == null)
        {
            return;
        }

        Vector2 moveDirection = new Vector2(1, -1).normalized; // -45度の方向ベクトル
        Vector2 projectedAnchoredPosition = Vector2.Dot(anchoredPosition, moveDirection) * moveDirection;
        Vector2 projectedDiffPreFramePosition = Vector2.Dot(diffPreFramePosition, moveDirection) * moveDirection;

        int safetyCounter = 0; // デバッグ用カウンター

        while (Vector2.Dot(projectedAnchoredPosition - projectedDiffPreFramePosition, moveDirection) < -itemScale * 2)
        {
            if (++safetyCounter > 1000) break; // ループの実行回数を制限

            diffPreFramePosition -= itemScale * moveDirection;

            var item = itemList.First.Value;
            itemList.RemoveFirst();
            itemList.AddLast(item);

            var pos = itemScale * (instantateItemCount + currentItemNo);
            item.anchoredPosition = pos * moveDirection; // 対角方向に配置

            onUpdateItem.Invoke(currentItemNo + instantateItemCount, item.gameObject);

            currentItemNo++;
        }

        safetyCounter = 0; // デバッグ用カウンターリセット

        while (Vector2.Dot(projectedAnchoredPosition - projectedDiffPreFramePosition, moveDirection) > 0)
        {
            if (++safetyCounter > 1000) break; // ループの実行回数を制限

            diffPreFramePosition += itemScale * moveDirection;

            var item = itemList.Last.Value;
            itemList.RemoveLast();
            itemList.AddFirst(item);

            currentItemNo--;

            var pos = itemScale * currentItemNo;
            item.anchoredPosition = pos * moveDirection; // 対角方向に配置

            onUpdateItem.Invoke(currentItemNo, item.gameObject);
        }
    }

    [System.Serializable]
    public class OnItemPositionChange : UnityEngine.Events.UnityEvent<int, GameObject> { }
}
