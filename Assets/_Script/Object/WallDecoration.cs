using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDecoration : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private Renderer[] objectForColor;
    [SerializeField] private Renderer[] wallForColor;

    private UIHandler uiHandler;
    private RaycastHandler raycastHandler;
    private InputHandler inputHandler;
    private FurnitureHandler furnitureHandler;
    private WallHandler wallHandler;

    public GameObject wallAttached;
    private Vector3 lockedPosition;
    private Material matOnWall;
    private bool isNewPositionLocked;
    private bool objectGrabbed;
    private bool isDecorNotAtDoor;
    private float offsetPosition = 0.0f;
    private float offsetHeightPosition = 0.5f;

    void Start()
    {
        uiHandler = FindObjectOfType<UIHandler>();
        raycastHandler = FindObjectOfType<RaycastHandler>();
        inputHandler = FindObjectOfType<InputHandler>();
        furnitureHandler = FindObjectOfType<FurnitureHandler>();
        wallHandler = FindObjectOfType<WallHandler>();

        isDecorNotAtDoor = true;
        matOnWall = wallForColor[0].material;
    }

    void Update()
    {
        SetGridPosition();
        HoveringWall();

        if (inputHandler.TriggerClick())
        {
            if (furnitureHandler.IsGrabingFurniture())
                LockPosition();
            else
                UnlockPosition();

            LockPaint();
        }

        if (inputHandler.TriggerDelete())
        {
            RemoveWallDecoration();
        }
    }

    void SetGridPosition()
    {
        if (objectGrabbed)
        {
            if (raycastHandler.GetWallObject() != null)
            {
                wallAttached = raycastHandler.GetWallObject();
                gameObject.transform.position = wallAttached.transform.position;
                gameObject.transform.rotation = wallAttached.transform.rotation;
            }
            else
            {
                gameObject.transform.position = new Vector3(raycastHandler.GetGridPositionWall().x - offsetPosition,
                                                            offsetHeightPosition,
                                                            raycastHandler.GetGridPositionWall().z - offsetPosition);
                uiHandler.WarningMessage("Must put on wall");
            }
        }
    }

    void LockPosition()
    {
        if (!isDecorNotAtDoor && objectGrabbed)
        {
            gameObject.transform.position = wallAttached.transform.position;
            lockedPosition = gameObject.transform.position;
            isNewPositionLocked = true;
            objectGrabbed = false;
            furnitureHandler.SetGrabStatus(null, false);
            Destroy(wallAttached);
        }
    }

    void UnlockPosition()
    {
        if (gameObject == raycastHandler.GetWallDecorObject() && !furnitureHandler.IsGrabingFurniture() && !wallHandler.IsPainting())
        {
            objectGrabbed = true;
            furnitureHandler.SetGrabStatus(gameObject, true);
            GameObject dummyObj = wallHandler.CreateWall();
            dummyObj.transform.position = lockedPosition;
            dummyObj.transform.rotation = gameObject.transform.rotation;
        }
    }

    void HoveringWall()
    {
        if (wallHandler.IsPainting())
        {
            if (gameObject == raycastHandler.GetWallObject())
                ChangeWallMaterial(wallHandler.GetPaintingMaterial(), false);
            else
                ChangeWallMaterial(matOnWall, true);
        }
    }

    void LockPaint()
    {
        if (wallHandler.IsPainting())
        {
            if (gameObject == raycastHandler.GetWallObject())
                ChangeWallMaterial(wallHandler.GetPaintingMaterial(), true);
        }
    }

    void RemoveWallDecoration()
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

    public void ResetWallDecor()
    {
        if (isNewPositionLocked)
        {
            gameObject.transform.position = lockedPosition; 
            objectGrabbed = false;
            furnitureHandler.SetGrabStatus(null, false);
        }
        else
            RemoveWallDecoration();
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

    public Material GetWallMaterial()
    {
        return wallForColor[0].sharedMaterial;
    }

    public void ChangeMaterial(Material material)
    {
        for (int i = 0; i < objectForColor.Length; i++)
        {
            objectForColor[i].material = material;
        }
    }

    public void ChangeWallMaterial(Material material, bool isLockPaint)
    {
        for (int i = 0; i < wallForColor.Length; i++)
        {
            wallForColor[i].material = material;
        }
        if (isLockPaint)
            matOnWall = material;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            isDecorNotAtDoor = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
        {
            isDecorNotAtDoor = true;
        }
    }
}
