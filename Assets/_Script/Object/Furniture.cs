using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private Renderer[] objectForColor;

    private UIHandler uiHandler;
    private RaycastHandler raycastHandler;
    private InputHandler inputHandler;
    private FurnitureHandler furnitureHandler;

    private Vector3 lockedPosition;
    private bool isNewPositionLocked;
    private bool objectGrabbed;
    private bool isFurnitureObstructedOrOutside;
    private float offsetPosition = 0.0f;
    private float offsetHeightPosition = 0.5f;

    void Start()
    {
        uiHandler = FindObjectOfType<UIHandler>();
        raycastHandler = FindObjectOfType<RaycastHandler>();
        inputHandler = FindObjectOfType<InputHandler>();
        furnitureHandler = FindObjectOfType<FurnitureHandler>();
    }

    void Update()
    {
        SetGridPosition();

        if (inputHandler.PressingRotateObjectLeft())
        {
            RotateFurniture(false);
        }
        else if (inputHandler.PressingRotateObjectRight())
        {
            RotateFurniture(true);
        }

        if (inputHandler.TriggerClick())
        {
            if (furnitureHandler.IsGrabingFurniture())
                LockPosition();
            else
                UnlockPosition();
        }

        if (inputHandler.TriggerDelete())
        {
            RemoveFurniture();
        }
    }

    void SetGridPosition()
    {
        if (objectGrabbed)
            gameObject.transform.position = new Vector3(raycastHandler.GetGridPositionFurniture().x - offsetPosition, 
                                                        offsetHeightPosition,
                                                        raycastHandler.GetGridPositionFurniture().z - offsetPosition);
    }

    void LockPosition()
    {
        if (!isFurnitureObstructedOrOutside && objectGrabbed)
        {
            lockedPosition = gameObject.transform.position;
            isNewPositionLocked = true;
            objectGrabbed = false;
            furnitureHandler.SetGrabStatus(null, false);
        }
    }

    void UnlockPosition()
    {
        if (gameObject == raycastHandler.GetFurnitureObject() && !furnitureHandler.IsGrabingFurniture())
        {
            objectGrabbed = true;
            furnitureHandler.SetGrabStatus(gameObject, true);
        }
    }

    void RotateFurniture(bool goRight)
    {
        if (objectGrabbed)
        {
            if (goRight)
                gameObject.transform.Rotate(new Vector3(0f, 1f, 0f));
            else
                gameObject.transform.Rotate(new Vector3(0f, -1f, 0f));
        }
    }

    void RemoveFurniture()
    {
        if (objectGrabbed)
        {
            furnitureHandler.SetGrabStatus(null, false);
            Destroy(gameObject);
        }
    }

    public void Grab()
    {
        objectGrabbed = true;
    }

    public void ResetFurniture()
    {
        if (isNewPositionLocked)
        {
            gameObject.transform.position = lockedPosition; 
            objectGrabbed = false;
            furnitureHandler.SetGrabStatus(null, false);
        }
        else
            RemoveFurniture();
    }

    public string GetName()
    {
        return itemName;
    }

    public string GetDescription()
    {
        return itemDescription;
    }

    public Material GetMaterial()
    {
        return objectForColor[0].sharedMaterial;
    }

    public void ChangeMaterial(Material material)
    {
        for (int i = 0; i < objectForColor.Length; i++)
        {
            objectForColor[i].material = material;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Furniture" || other.tag == "Wall" || other.tag == "WallDecoration")
        {
            isFurnitureObstructedOrOutside = true;
            uiHandler.WarningMessage("Object Obstructed");
        }
        else if (other.tag == "Grid")
        {
            isFurnitureObstructedOrOutside = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Furniture" || other.tag == "Wall" || other.tag == "WallDecoration")
        {
            isFurnitureObstructedOrOutside = false;
        }
        else if (other.tag == "Grid")
        {
            isFurnitureObstructedOrOutside = true;
            uiHandler.WarningMessage("Outside Area");
        }
    }
}
