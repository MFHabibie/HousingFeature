using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WallDecorItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject itemToSpawn;

    [SerializeField] private Material[] colorType;

    private Button ButtonOnClick;
    private Image imagePreview;
    private WallDecoration wallDecoration;
    private FurnitureHandler furnitureHandler;
    private DetailItemHandler detailHandler;

    void Start()
    {
        furnitureHandler = FindObjectOfType<FurnitureHandler>();
        detailHandler = FindObjectOfType<DetailItemHandler>();
        ButtonOnClick = GetComponent<Button>();
        wallDecoration = itemToSpawn.GetComponent<WallDecoration>();

        ButtonOnClick.onClick.AddListener(SelectWallDecor);
    }

    public void SetupPreview(Texture2D image)
    {
        Sprite imageSprite = Sprite.Create(image, new Rect(0f, 0f, image.width, image.height), new Vector2(0.5f, 0.5f));
        imagePreview = GetComponent<Image>();
        imagePreview.sprite = imageSprite;
    }

    void SelectWallDecor()
    {
        furnitureHandler.SpawnFurniture(itemToSpawn);
        detailHandler.HideDetail();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        detailHandler.SetOnFocus(true);
        detailHandler.ShowingDetail(this, wallDecoration.GetName(), wallDecoration.GetDescription(), colorType);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Invoke("DelayOnHide", 2f);
    }

    void DelayOnHide()
    {
        if (!detailHandler.GetOnFocus())
            detailHandler.HideDetail();
    }
}
