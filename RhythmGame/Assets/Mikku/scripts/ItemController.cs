using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemImage; // 表示する画像
    public Text itemText; // 表示するテキスト

    private DetailPanel detailPanel;

    private void Start()
    {
        detailPanel = FindObjectOfType<DetailPanel>();
        if (detailPanel == null)
        {
            Debug.LogError("DetailPanel not found in the scene.");
        }
        else
        {
            Debug.Log("DetailPanel found: " + detailPanel.gameObject.name);
        }

        if (itemImage == null || itemText == null)
        {
            Debug.LogError("ItemImage or ItemText is not assigned on " + gameObject.name);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (detailPanel != null)
        {
            Debug.Log("Showing item: " + itemText.text);
            detailPanel.ShowItem(itemImage.sprite, itemText.text);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (detailPanel != null)
        {
            detailPanel.HideItem();
        }
    }
}