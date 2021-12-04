using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FurnitureItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject itemToSpawn;

    [SerializeField] private Material[] colorType;

    private Button ButtonOnClick;
    private Image imagePreview;
    private Furniture furniture;
    private FurnitureHandler furnitureHandler;
    private DetailItemHandler detailHandler;

    void Start()
    {
        furnitureHandler = FindObjectOfType<FurnitureHandler>();
        detailHandler = FindObjectOfType<DetailItemHandler>();
        ButtonOnClick = GetComponent<Button>();
        furniture = itemToSpawn.GetComponent<Furniture>();

        ButtonOnClick.onClick.AddListener(SelectFurniture);
    }

    public void SetupPreview(Texture2D image)
    {
        Sprite imageSprite = Sprite.Create(image, new Rect(0f, 0f, image.width, image.height), new Vector2(0.5f, 0.5f));
        imagePreview = GetComponent<Image>();
        imagePreview.sprite = imageSprite;
    }

    void SelectFurniture()
    {
        furnitureHandler.SpawnFurniture(itemToSpawn);
        detailHandler.HideDetail();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        detailHandler.SetOnFocus(true);
        detailHandler.ShowingDetail(this, furniture.GetName(), furniture.GetDescription(), colorType);
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
