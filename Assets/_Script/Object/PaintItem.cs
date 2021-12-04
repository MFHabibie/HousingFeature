using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintItem : MonoBehaviour
{
    private WallHandler wallHandler;

    private Material materialOnButton;
    private Button ButtonOnClick;
    private Image imagePreview;

    void Start()
    {
        wallHandler = FindObjectOfType<WallHandler>();

        ButtonOnClick = GetComponent<Button>();

        ButtonOnClick.onClick.AddListener(SetPainting);
    }

    public void SetupPreview(Material mat)
    {
        //Sprite imageSprite = Sprite.Create(image, new Rect(0f, 0f, image.width, image.height), new Vector2(0.5f, 0.5f));
        imagePreview = GetComponent<Image>();
        imagePreview.color = mat.GetColor("_Color");
        materialOnButton = mat;
    }

    void SetPainting()
    {
        wallHandler.SetPaintingMaterial(materialOnButton);
    }
}
