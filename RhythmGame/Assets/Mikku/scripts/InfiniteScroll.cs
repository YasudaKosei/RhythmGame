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
    public int instantateItemCount;

    [SerializeField]
    private Direction direction;

    public OnItemPositionChange onUpdateItem = new OnItemPositionChange();

    [System.NonSerialized]
    public LinkedList<RectTransform> itemList = new LinkedList<RectTransform>();

    protected float diffPreFramePosition = 0;

    protected int currentItemNo = 0;

    public enum Direction
    {
        Vertical,
        Horizontal,
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

    private float anchoredPosition
    {
        get
        {
            return direction == Direction.Vertical ? -rectTransform.anchoredPosition.y : rectTransform.anchoredPosition.x;
        }
    }

    private float _itemScale = -1;
    public float itemScale
    {
        get
        {
            if (itemPrototype != null && _itemScale == -1)
            {
                _itemScale = direction == Direction.Vertical ? itemPrototype.sizeDelta.y : itemPrototype.sizeDelta.x;
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
        scrollRect.horizontal = direction == Direction.Horizontal;
        scrollRect.vertical = direction == Direction.Vertical;
        scrollRect.content = rectTransform;

        itemPrototype.gameObject.SetActive(false);

        for (int i = 0; i < instantateItemCount; i++)
        {
            var item = GameObject.Instantiate(itemPrototype) as RectTransform;
            item.SetParent(transform, false);
            item.name = i.ToString();
            item.anchoredPosition = direction == Direction.Vertical ? new Vector2(0, -itemScale * i) : new Vector2(itemScale * i, 0);
            itemList.AddLast(item);

            // EventTriggerÇÃí«â¡
            var eventTrigger = item.gameObject.AddComponent<EventTrigger>();

            var pointerEnter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
            pointerEnter.callback.AddListener((eventData) => OnPointerEnter(item));
            eventTrigger.triggers.Add(pointerEnter);

            var pointerExit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
            pointerExit.callback.AddListener((eventData) => OnPointerExit(item));
            eventTrigger.triggers.Add(pointerExit);

            // Scroll handling
            var pointerDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
            pointerDown.callback.AddListener((eventData) => OnPointerDown(eventData));
            eventTrigger.triggers.Add(pointerDown);

            var drag = new EventTrigger.Entry { eventID = EventTriggerType.Drag };
            drag.callback.AddListener((eventData) => OnDrag(eventData));
            eventTrigger.triggers.Add(drag);

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

    private void OnPointerEnter(RectTransform item)
    {
        item.localScale = Vector3.one * 1.2f; // ägëÂ
    }

    private void OnPointerExit(RectTransform item)
    {
        item.localScale = Vector3.one; // å≥ÇÃÉTÉCÉYÇ…ñﬂÇ∑
    }

    private void OnPointerDown(BaseEventData eventData)
    {
        var pointerEventData = eventData as PointerEventData;
        if (pointerEventData != null)
        {
            // Pass the event to the ScrollRect
            var scrollRect = GetComponentInParent<ScrollRect>();
            if (scrollRect != null)
            {
                scrollRect.OnBeginDrag(pointerEventData);
            }
        }
    }

    private void OnDrag(BaseEventData eventData)
    {
        var pointerEventData = eventData as PointerEventData;
        if (pointerEventData != null)
        {
            // Pass the event to the ScrollRect
            var scrollRect = GetComponentInParent<ScrollRect>();
            if (scrollRect != null)
            {
                scrollRect.OnDrag(pointerEventData);
            }
        }
    }

    void Update()
    {
        if (itemList.First == null)
        {
            return;
        }

        while (anchoredPosition - diffPreFramePosition < -itemScale * 2)
        {
            diffPreFramePosition -= itemScale;

            var item = itemList.First.Value;
            itemList.RemoveFirst();
            itemList.AddLast(item);

            var pos = itemScale * instantateItemCount + itemScale * currentItemNo;
            item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);

            onUpdateItem.Invoke(currentItemNo + instantateItemCount, item.gameObject);

            currentItemNo++;
        }

        while (anchoredPosition - diffPreFramePosition > 0)
        {
            diffPreFramePosition += itemScale;

            var item = itemList.Last.Value;
            itemList.RemoveLast();
            itemList.AddFirst(item);

            currentItemNo--;

            var pos = itemScale * currentItemNo;
            item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);
            onUpdateItem.Invoke(currentItemNo, item.gameObject);
        }
    }

    [System.Serializable]
    public class OnItemPositionChange : UnityEngine.Events.UnityEvent<int, GameObject> { }
}