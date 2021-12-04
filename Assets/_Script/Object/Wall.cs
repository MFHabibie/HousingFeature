using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wall : MonoBehaviour
{
    [SerializeField] private string itemName;

    private InputHandler inputHandler;
    private RaycastHandler raycastHandler;
    private WallHandler wallHandler;

    private Material matOnWall;

    void Start()
    {
        inputHandler = FindObjectOfType<InputHandler>();
        raycastHandler = FindObjectOfType<RaycastHandler>();
        wallHandler = FindObjectOfType<WallHandler>();

        matOnWall = GetComponentInChildren<MeshRenderer>().material;
    }
    
    void Update()
    {
        HoveringWall();

        if (inputHandler.TriggerClick())
        {
            LockPaint();
        }
    }

    public string GetName()
    {
        return itemName;
    }

    public Material GetMaterial()
    {
        return gameObject.GetComponentInChildren<Renderer>().sharedMaterial;
    }

    void HoveringWall()
    {
        if (wallHandler.IsPainting())
        {
            if (gameObject == raycastHandler.GetWallObject())
                ChangeMaterial(wallHandler.GetPaintingMaterial(), false);
            else
                ChangeMaterial(matOnWall, true);
        }
    }

    void LockPaint()
    {
        if (wallHandler.IsPainting())
        {
            if (gameObject == raycastHandler.GetWallObject())
                ChangeMaterial(wallHandler.GetPaintingMaterial(), true);
        }
    }

    public void ChangeMaterial(Material material, bool isLockPaint)
    {
        GetComponentInChildren<Renderer>().material = material;
        if (isLockPaint)
            matOnWall = material;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WallDecoration")
        {
            GetComponentInChildren<Renderer>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "WallDecoration")
        {
            GetComponentInChildren<Renderer>().enabled = true;
        }
    }
}
