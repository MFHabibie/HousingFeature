using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject itemForMaterial;
    [SerializeField] private GameObject itemListPanel;
    [SerializeField] private GameObject savePanel;
    [SerializeField] private GameObject loadPanel;
    [SerializeField] private Button createWallBtn;
    [SerializeField] private Button paintWallBtn;
    [SerializeField] private Button wallDecorBtn;
    [SerializeField] private Button furnitureBtn;


    [SerializeField] private Button saveBtn;
    [SerializeField] private Button loadBtn;
    [SerializeField] private Text saveTxt;
    [SerializeField] private Text loadTxt;
    [SerializeField] private Button exitSaveBtn;
    [SerializeField] private Button exitLoadBtn;
    [SerializeField] private Button confirmSave;
    [SerializeField] private Button confirmLoad;
    [SerializeField] private Button destroyWallBtn;
    [SerializeField] private GameObject warningObj;

    private FurnitureHandler furnitureHandler;
    private WallHandler wallHandler;
    private DestroyWallHandler destroyWallHandler;
    private DetailItemHandler detailHandler;
    private DataSavingHandler dataSavingHandler;

    void Start()
    {
        furnitureHandler = FindObjectOfType<FurnitureHandler>();
        wallHandler = FindObjectOfType<WallHandler>();
        destroyWallHandler = FindObjectOfType<DestroyWallHandler>();
        detailHandler = FindObjectOfType<DetailItemHandler>();
        dataSavingHandler = FindObjectOfType<DataSavingHandler>();

        SetSaveMenu(false);
        SetLoadMenu(false);
        HideWarning();

        destroyWallBtn.onClick.AddListener(DestroyWall);
        createWallBtn.onClick.AddListener(LoadWall);
        paintWallBtn.onClick.AddListener(LoadWallTexture);
        furnitureBtn.onClick.AddListener(LoadFurniture);
        wallDecorBtn.onClick.AddListener(LoadWallDecor);
        saveBtn.onClick.AddListener(()=> SetSaveMenu(true));
        loadBtn.onClick.AddListener(() => SetLoadMenu(true));
        exitSaveBtn.onClick.AddListener(() => SetSaveMenu(false));
        exitLoadBtn.onClick.AddListener(() => SetLoadMenu(false));
        confirmSave.onClick.AddListener(() => ProceedSave(saveTxt.text));
        confirmLoad.onClick.AddListener(() => ProceedLoad(loadTxt.text));
    }

    void ResetPanel()
    {
        for (int i = 0; i < itemListPanel.transform.childCount; i++)
        {
            Destroy(itemListPanel.transform.GetChild(i).gameObject);
        }

        detailHandler.HideDetail();
        furnitureHandler.ResetFeature();
        wallHandler.ResetFeature();
        destroyWallHandler.ResetFeature();
    }

    void LoadWall()
    {
        ResetPanel();

        GameObject[] listWall = Resources.LoadAll<GameObject>("Wall/ItemList/");
        Texture2D[] listPreview = Resources.LoadAll<Texture2D>("Wall/Preview/");

        foreach (GameObject obj in listWall)
        {
            GameObject dummyObj = Instantiate(obj, itemListPanel.transform);
            Texture2D preview = LoadPreviewBasedName(listPreview, obj.name);
            dummyObj.GetComponent<WallItem>().SetupPreview(preview);
        }
    }

    void LoadWallTexture()
    {
        ResetPanel();

        Material[] listWallTexture = Resources.LoadAll<Material>("Wall/Material/");
        foreach (Material obj in listWallTexture)
        {
            GameObject dummyObj = Instantiate(itemForMaterial, itemListPanel.transform);
            dummyObj.GetComponent<PaintItem>().SetupPreview(obj);
        }
    }

    void LoadFurniture()
    {
        ResetPanel();

        GameObject[] listFurniture = Resources.LoadAll<GameObject>("Furniture/ItemList/");
        Texture2D[] listPreview = Resources.LoadAll<Texture2D>("Furniture/Preview/");

        foreach (GameObject obj in listFurniture)
        {
            GameObject dummyObj = Instantiate(obj, itemListPanel.transform);
            Texture2D preview = LoadPreviewBasedName(listPreview, obj.name);
            dummyObj.GetComponent<FurnitureItem>().SetupPreview(preview);
        }
    }

    void LoadWallDecor()
    {
        ResetPanel();

        GameObject[] listWallDecor = Resources.LoadAll<GameObject>("WallDecoration/ItemList/");
        Texture2D[] listPreview = Resources.LoadAll<Texture2D>("WallDecoration/Preview/");
        foreach (GameObject obj in listWallDecor)
        {
            GameObject dummyObj = Instantiate(obj, itemListPanel.transform);
            Texture2D preview = LoadPreviewBasedName(listPreview, obj.name);
            dummyObj.GetComponent<WallDecorItem>().SetupPreview(preview);
        }
    }

    void DestroyWall()
    {
        ResetPanel();
        destroyWallHandler.SetupDestroyWall();
    }

    Texture2D LoadPreviewBasedName (Texture2D[] listPreview, string name)
    {
        foreach (Texture2D image in listPreview)
        {
            if (image.name == name)
                return image;
        }

        return null;
    }

    void SetSaveMenu(bool isVisible)
    {
        savePanel.SetActive(isVisible);
    }

    void SetLoadMenu(bool isVisible)
    {
        loadPanel.SetActive(isVisible);
    }

    void ProceedSave(string saveName)
    {
        dataSavingHandler.SaveGame(saveName);
        SetSaveMenu(false);
    }

    void ProceedLoad(string loadName)
    {
        dataSavingHandler.LoadGame(loadName);
        SetLoadMenu(false);
    }

    public void WarningMessage(string text)
    {
        CancelInvoke("HideWarning");
        warningObj.GetComponentInChildren<Text>().text = text;
        warningObj.SetActive(true);
        Invoke("HideWarning", 2f);
    }

    void HideWarning()
    {
        warningObj.SetActive(false);
    }
}
