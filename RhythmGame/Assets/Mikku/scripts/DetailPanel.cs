using UnityEngine;
using UnityEngine.UI;

public class DetailPanel : MonoBehaviour
{
    public Image detailImage;
    public Text detailText;

    private void Awake()
    {
        if (detailImage == null)
        {
            detailImage = GetComponentInChildren<Image>();
        }
        if (detailText == null)
        {
            detailText = GetComponentInChildren<Text>();
        }
        Debug.Log("DetailPanel initialized: detailImage = " + detailImage + ", detailText = " + detailText);
    }

    public void ShowItem(Sprite image, string text)
    {
        Debug.Log("ShowItem called with image: " + image + ", text: " + text);
        detailImage.sprite = image;
        detailText.text = text;
        gameObject.SetActive(true);
    }

    public void HideItem()
    {
        detailImage.sprite = null;
        detailText.text = "";
        gameObject.SetActive(false);
    }
}