using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorItem : MonoBehaviour
{
    [SerializeField] private RawImage previewColor;

    private Material material;
    private Button ButtonColorClick;
    private FurnitureItem furnitureItem;
    private WallDecorItem wallDecorItem;
    private FurnitureHandler furnitureHandler;
    private DetailItemHandler detailHandler;

    void Start()
    {
        furnitureHandler = FindObjectOfType<FurnitureHandler>();
        detailHandler = FindObjectOfType<DetailItemHandler>();
        ButtonColorClick = GetComponent<Button>();

        ButtonColorClick.onClick.AddListener(SpawnWithColor);
    }

    public void SetupColor(FurnitureItem item, Material itemColor)
    {
        furnitureItem = item;
        material = itemColor;
        previewColor.color = itemColor.GetColor("_Color");
    }

    public void SetupColor(WallDecorItem item, Material itemColor)
    {
        wallDecorItem = item;
        material = itemColor;
        previewColor.color = itemColor.GetColor("_Color");
    }

    void SpawnWithColor()
    {
        if (furnitureItem != null)
            furnitureHandler.SpawnFurnitureWithColor(furnitureItem.itemToSpawn, material);
        else
            furnitureHandler.SpawnFurnitureWithColor(wallDecorItem.itemToSpawn, material);
        detailHandler.HideDetail();
    }
}
