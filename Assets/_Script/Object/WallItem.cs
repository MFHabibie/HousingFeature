using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WallItem : MonoBehaviour
{
    public GameObject itemToSpawn;

    private Button ButtonOnClick;
    private Image imagePreview;
    private Wall wall;
    private WallHandler wallHandler;

    void Start()
    {
        wallHandler = FindObjectOfType<WallHandler>();
        ButtonOnClick = GetComponent<Button>();
        wall = itemToSpawn.GetComponent<Wall>();

        ButtonOnClick.onClick.AddListener(SelectWall);
    }

    public void SetupPreview(Texture2D image)
    {
        Sprite imageSprite = Sprite.Create(image, new Rect(0f, 0f, image.width, image.height), new Vector2(0.5f, 0.5f));
        imagePreview = GetComponent<Image>();
        imagePreview.sprite = imageSprite;
    }

    void SelectWall()
    {
        wallHandler.SetupWall(itemToSpawn);
    }
}
