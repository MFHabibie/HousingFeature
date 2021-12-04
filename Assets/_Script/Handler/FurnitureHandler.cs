using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureHandler : MonoBehaviour
{
    [SerializeField] private GameObject placedForFurniture;

    private GameObject grabbedFurniture;
    private bool isGrabingFurniture;

    public void SpawnFurniture(GameObject selectedFurniture)
    {
        GameObject dummyFurniture = Instantiate(selectedFurniture, placedForFurniture.transform);
        if (dummyFurniture.GetComponent<Furniture>())
            dummyFurniture.GetComponent<Furniture>().Grab();
        else
            dummyFurniture.GetComponent<WallDecoration>().Grab();
        grabbedFurniture = dummyFurniture;

        isGrabingFurniture = true;
    }

    public void SpawnFurnitureWithColor(GameObject selectedFurniture, Material material)
    {
        GameObject dummyFurniture = Instantiate(selectedFurniture, placedForFurniture.transform);
        if (dummyFurniture.GetComponent<Furniture>())
        {
            dummyFurniture.GetComponent<Furniture>().ChangeMaterial(material);
            dummyFurniture.GetComponent<Furniture>().Grab();
        }
        else
        {
            dummyFurniture.GetComponent<WallDecoration>().ChangeMaterial(material);
            dummyFurniture.GetComponent<WallDecoration>().Grab();
        }
        grabbedFurniture = dummyFurniture;

        isGrabingFurniture = true;
    }

    public void ResetFeature()
    {
        isGrabingFurniture = false;
        if (grabbedFurniture != null)
        {
            if (grabbedFurniture.GetComponent<Furniture>())
                grabbedFurniture.GetComponent<Furniture>().ResetFurniture();
            else
                grabbedFurniture.GetComponent<WallDecoration>().ResetWallDecor();
        }
    }

    public void SetGrabStatus(GameObject obj, bool value)
    {
        grabbedFurniture = obj;
        isGrabingFurniture = value;
    }

    public bool IsGrabingFurniture()
    {
        return grabbedFurniture;
    }

    public GameObject GetFurniturePlace()
    {
        return placedForFurniture;
    }
}
