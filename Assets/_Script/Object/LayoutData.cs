using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LayoutData
{
    public string[] furnitureName;
    public string[] furnitureColorName;
    public float[][] furniturePosition;
    public float[][] furnitureRotation;

    public string[] wallName;
    public string[] wallColorName;
    public float[][] wallPosition;
    public float[][] wallRotation;

    public string[] wallDecorName;
    public string[] wallDecorColorName;
    public string[] wallOnDecorColorName;
    public float[][] wallDecorPosition;
    public float[][] wallDecorRotation;

    public LayoutData (DataSavingHandler data)
    {
        if (data.furniture.Length != 0)
        {
            furnitureName = new string[data.furniture.Length];
            furnitureColorName = new string[data.furniture.Length];
            furniturePosition = new float[data.furniture.Length][];
            furnitureRotation = new float[data.furniture.Length][];
            for (int i = 0; i < data.furniture.Length; i++)
            {
                furnitureName[i] = data.furniture[i].GetName();

                furnitureColorName[i] = data.furniture[i].GetMaterial().name;

                furniturePosition[i] = new float[3];
                furniturePosition[i][0] = data.furniture[i].gameObject.transform.position.x;
                furniturePosition[i][1] = data.furniture[i].gameObject.transform.position.y;
                furniturePosition[i][2] = data.furniture[i].gameObject.transform.position.z;

                furnitureRotation[i] = new float[3];
                furnitureRotation[i][0] = data.furniture[i].gameObject.transform.eulerAngles.x;
                furnitureRotation[i][1] = data.furniture[i].gameObject.transform.eulerAngles.y;
                furnitureRotation[i][2] = data.furniture[i].gameObject.transform.eulerAngles.z;
            }
        }

        if (data.wall.Length != 0)
        {
            wallName = new string[data.wall.Length];
            wallColorName = new string[data.wall.Length];
            wallPosition = new float[data.wall.Length][];
            wallRotation = new float[data.wall.Length][];
            for (int i = 0; i < data.wall.Length; i++)
            {
                wallName[i] = data.wall[i].GetName();

                wallColorName[i] = data.wall[i].GetMaterial().name;

                wallPosition[i] = new float[3];
                wallPosition[i][0] = data.wall[i].gameObject.transform.position.x;
                wallPosition[i][1] = data.wall[i].gameObject.transform.position.y;
                wallPosition[i][2] = data.wall[i].gameObject.transform.position.z;

                wallRotation[i] = new float[3];
                wallRotation[i][0] = data.wall[i].gameObject.transform.eulerAngles.x;
                wallRotation[i][1] = data.wall[i].gameObject.transform.eulerAngles.y;
                wallRotation[i][2] = data.wall[i].gameObject.transform.eulerAngles.z;
            }
        }

        if (data.wallDecoration.Length != 0)
        {
            wallDecorName = new string[data.wallDecoration.Length];
            wallDecorColorName = new string[data.wallDecoration.Length];
            wallOnDecorColorName = new string[data.wallDecoration.Length];
            wallDecorPosition = new float[data.wallDecoration.Length][];
            wallDecorRotation = new float[data.wallDecoration.Length][];
            for (int i = 0; i < data.wallDecoration.Length; i++)
            {
                wallDecorName[i] = data.wallDecoration[i].GetName();

                wallDecorColorName[i] = data.wallDecoration[i].GetMaterial().name;
                wallOnDecorColorName[i] = data.wallDecoration[i].GetWallMaterial().name;

                wallDecorPosition[i] = new float[3];
                wallDecorPosition[i][0] = data.wallDecoration[i].gameObject.transform.position.x;
                wallDecorPosition[i][1] = data.wallDecoration[i].gameObject.transform.position.y;
                wallDecorPosition[i][2] = data.wallDecoration[i].gameObject.transform.position.z;

                wallDecorRotation[i] = new float[3];
                wallDecorRotation[i][0] = data.wallDecoration[i].gameObject.transform.eulerAngles.x;
                wallDecorRotation[i][1] = data.wallDecoration[i].gameObject.transform.eulerAngles.y;
                wallDecorRotation[i][2] = data.wallDecoration[i].gameObject.transform.eulerAngles.z;
            }
        }
    }
}
