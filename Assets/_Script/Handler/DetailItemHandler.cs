using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DetailItemHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text furnitureName;
    [SerializeField] private Text furnitureDescription;
    [SerializeField] private GameObject colorPanel;
    [SerializeField] private GameObject colorItem;

    private bool isOnFocus;

    void Start()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void ShowingDetail(FurnitureItem furnitureItem, string name, string description, Material[] colorType)
    {
        transform.position = new Vector3(furnitureItem.transform.position.x * 0.5f, transform.position.y, transform.position.z);

        ResetColorType();

        furnitureName.text = name;
        furnitureDescription.text = description;

        for (int i = 0; i < colorType.Length; i++)
        {
            GameObject dummyColorItem = Instantiate(colorItem, colorPanel.transform);
            dummyColorItem.GetComponent<ColorItem>().SetupColor(furnitureItem, colorType[i]);
        }

        gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void ShowingDetail(WallDecorItem furnitureItem, string name, string description, Material[] colorType)
    {
        transform.position = new Vector3(furnitureItem.transform.position.x * 0.5f, transform.position.y, transform.position.z);
        ResetColorType();

        furnitureName.text = name;
        furnitureDescription.text = description;

        for (int i = 0; i < colorType.Length; i++)
        {
            GameObject dummyColorItem = Instantiate(colorItem, colorPanel.transform);
            dummyColorItem.GetComponent<ColorItem>().SetupColor(furnitureItem, colorType[i]);
        }

        gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void HideDetail()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

    void ResetColorType()
    {
        for (int i = 0; i < colorPanel.transform.childCount; i++)
        {
            Destroy(colorPanel.transform.GetChild(i).gameObject);
        }
    }

    public bool GetOnFocus()
    {
        return isOnFocus;
    }

    public void SetOnFocus(bool value)
    {
        isOnFocus = value;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.GetComponent<CanvasGroup>().alpha != 0)
        {
            isOnFocus = true;
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOnFocus = false;
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }
}
