using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHandler : MonoBehaviour
{
    [SerializeField] private GameObject wallPointToSpawn;

    private RaycastHandler raycastHandler;
    private InputHandler inputHandler;

    private GameObject wallToSpawn;

    private GameObject wallPoint;
    private Transform lastPoint;
    private bool isCreatingActive;
    private bool isOnCreating;

    private bool isOnPainting;
    private Material materialActive;

    private float offsetPosition = 0f;
    private float offsetHeightPosition = 0.5f;

    void Start()
    {
        raycastHandler = FindObjectOfType<RaycastHandler>();
        inputHandler = FindObjectOfType<InputHandler>();
    }


    void Update()
    {
        SetGridPointPosition();

        if (inputHandler.TriggerClick())
        {
            StartCreatingWall();
        }
        else if (inputHandler.ReleaseTriggerClick())
        {
            SetWall();
        }
        else
        {
            if (isOnCreating)
            {
                UpdateWall();
            }
        }

        if (inputHandler.TriggerCancel())
        {
            StopCreating();
        }
    }

    public void SetupWall(GameObject obj)
    {
        wallToSpawn = obj;
        isCreatingActive = true;

        Vector3 startWallPosition = new Vector3(raycastHandler.GetGridPositionWall().x + offsetPosition,
                                                offsetHeightPosition,
                                                raycastHandler.GetGridPositionWall().z + offsetPosition);
        wallPoint = Instantiate(wallPointToSpawn, startWallPosition, Quaternion.identity);
    }

    void SetGridPointPosition()
    {
        if (isCreatingActive)
        {
            Vector3 pointGridPosition = new Vector3(raycastHandler.GetGridPositionWall().x + offsetPosition,
                                                    offsetHeightPosition,
                                                    raycastHandler.GetGridPositionWall().z + offsetPosition);
            wallPoint.transform.position = pointGridPosition;
        }
    }

    void StartCreatingWall()
    {
        if (isCreatingActive)
        {
            isOnCreating = true;

            lastPoint = new GameObject().transform;
            lastPoint.position = wallPoint.transform.position;
        }
    }

    void SetWall()
    {
        isOnCreating = false;
    }

    void UpdateWall()
    {
        Vector3 currentWallPosition = wallPoint.transform.position;
        if (!currentWallPosition.Equals(lastPoint.transform.position))
        {
            CreateNextWall(currentWallPosition);
        }
    }

    void CreateNextWall(Vector3 currentPosition)
    {
        GameObject nextWall = Instantiate(wallToSpawn, currentPosition, Quaternion.identity);
        nextWall.transform.LookAt(lastPoint.transform);
        lastPoint = nextWall.transform;
    }

    public GameObject CreateWall()
    {
        return Instantiate(wallToSpawn);
    }

    public void StopCreating()
    {
        isCreatingActive = false;
        Destroy(wallPoint);
    }

    public void ResetFeature()
    {
        StopCreating();
        SetIsPainting(false);
    }

    void SetIsPainting(bool value)
    {
        isOnPainting = value;
    }

    public bool IsPainting()
    {
        return isOnPainting;
    }

    public void SetPaintingMaterial(Material mat)
    {
        materialActive = mat;
        SetIsPainting(true);
    }

    public Material GetPaintingMaterial()
    {
        return materialActive;
    }
}
