using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataSavingHandler : MonoBehaviour
{
    private UIHandler uiHandler;

    public Furniture[] furniture;
    public Wall[] wall;
    public WallDecoration[] wallDecoration;

    private FurnitureHandler furnitureHandler;

    private void Start()
    {
        uiHandler = FindObjectOfType<UIHandler>();
        furnitureHandler = FindObjectOfType<FurnitureHandler>();
    }

    public void SaveGame(string name)
    {
        furniture = FindObjectsOfType<Furniture>();
        wall = FindObjectsOfType<Wall>();
        wallDecoration = FindObjectsOfType<WallDecoration>();

        string path = Application.streamingAssetsPath + "/" + name + ".layout";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream streamer = new FileStream(path, FileMode.Create);

        LayoutData data = new LayoutData(this);

        formatter.Serialize(streamer, data);
        streamer.Close();
    }

    public void LoadGame(string name)
    {

        string path = Application.streamingAssetsPath + "/" + name + ".layout";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream streamer = new FileStream(path, FileMode.Open);

            LayoutData loadData = formatter.Deserialize(streamer) as LayoutData;
            streamer.Close();

            UpdateLayout(loadData);
        }
        else
        {
            print("File not found. Path: " + path);
        }
    }

    void UpdateLayout(LayoutData data)
    {
        Furniture[] listFurniture = Resources.LoadAll<Furniture>("Furniture/");
        Material[] listFurnitureMaterial = Resources.LoadAll<Material>("Furniture/Material/");
        if (data.furnitureName != null)
        {
            for (int i = 0; i < data.furnitureName.Length; i++)
            {
                GameObject dummyObj = GetFurnitureFromName(data.furnitureName[i], listFurniture);
                if (dummyObj)
                {
                    Vector3 position = new Vector3(data.furniturePosition[i][0], data.furniturePosition[i][1], data.furniturePosition[i][2]);
                    Vector3 eulerRotation = new Vector3(data.furnitureRotation[i][0], data.furnitureRotation[i][1], data.furnitureRotation[i][2]);
                    GameObject spawnedObj = Instantiate(dummyObj, position, Quaternion.Euler(eulerRotation), furnitureHandler.GetFurniturePlace().transform);

                    Material dummyMat = GetMaterialFromName(data.furnitureColorName[i], listFurnitureMaterial);
                    if (dummyMat)
                        spawnedObj.GetComponent<Furniture>().ChangeMaterial(dummyMat);
                }
            }
        }

        Wall[] listWall = Resources.LoadAll<Wall>("Wall/");
        Material[] listWallMaterial = Resources.LoadAll<Material>("Wall/Material/");
        if (data.wallName != null)
        {
            for (int i = 0; i < data.wallName.Length; i++)
            {
                GameObject dummyObj = GetWallFromName(data.wallName[i], listWall);
                if (dummyObj)
                {
                    Vector3 position = new Vector3(data.wallPosition[i][0], data.wallPosition[i][1], data.wallPosition[i][2]);
                    Vector3 eulerRotation = new Vector3(data.wallRotation[i][0], data.wallRotation[i][1], data.wallRotation[i][2]);
                    GameObject spawnedObj = Instantiate(dummyObj, position, Quaternion.Euler(eulerRotation), furnitureHandler.GetFurniturePlace().transform);

                    Material dummyMat = GetMaterialFromName(data.wallColorName[i], listWallMaterial);
                    if (dummyMat)
                        spawnedObj.GetComponent<Wall>().ChangeMaterial(dummyMat, true);
                }
            }
        }

        WallDecoration[] listWallDecor = Resources.LoadAll<WallDecoration>("WallDecoration/");
        Material[] listWallDecorMaterial = Resources.LoadAll<Material>("WallDecoration/Material/");
        if (data.wallName != null)
        {
            for (int i = 0; i < data.wallDecorName.Length; i++)
            {
                GameObject dummyObj = GetWallDecorFromName(data.wallDecorName[i], listWallDecor);
                if (dummyObj)
                {
                    Vector3 position = new Vector3(data.wallDecorPosition[i][0], data.wallDecorPosition[i][1], data.wallDecorPosition[i][2]);
                    Vector3 eulerRotation = new Vector3(data.wallDecorRotation[i][0], data.wallDecorRotation[i][1], data.wallDecorRotation[i][2]);
                    GameObject spawnedObj = Instantiate(dummyObj, position, Quaternion.Euler(eulerRotation));

                    Material dummyDecorMat = GetMaterialFromName(data.wallDecorColorName[i], listWallDecorMaterial);
                    if (dummyDecorMat)
                        spawnedObj.GetComponent<WallDecoration>().ChangeMaterial(dummyDecorMat);

                    Material dummyWallOnDecorMat = GetMaterialFromName(data.wallOnDecorColorName[i], listWallMaterial);
                    if (dummyWallOnDecorMat)
                        spawnedObj.GetComponent<WallDecoration>().ChangeWallMaterial(dummyWallOnDecorMat, true);
                }
            }
        }
    }

    GameObject GetFurnitureFromName(string name, Furniture[] objToLook)
    {
        for (int i = 0; i < objToLook.Length; i++)
        {
            if(objToLook[i].GetName() == name)
            {
                return objToLook[i].gameObject;
            }
        }

        return null;
    }

    GameObject GetWallFromName(string name, Wall[] objToLook)
    {
        for (int i = 0; i < objToLook.Length; i++)
        {
            if (objToLook[i].GetName() == name)
            {
                return objToLook[i].gameObject;
            }
        }

        return null;
    }

    GameObject GetWallDecorFromName(string name, WallDecoration[] objToLook)
    {
        for (int i = 0; i < objToLook.Length; i++)
        {
            if (objToLook[i].GetName() == name)
            {
                return objToLook[i].gameObject;
            }
        }

        return null;
    }

    Material GetMaterialFromName(string name, Material[] matToLook)
    {
        for (int i = 0; i < matToLook.Length; i++)
        {
            if (matToLook[i].name == name)
            {
                return matToLook[i];
            }
        }

        return null;
    }
}
