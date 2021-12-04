using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWallHandler : MonoBehaviour
{
    [SerializeField] private GameObject wallPointToSpawn;

    private RaycastHandler raycastHandler;
    private InputHandler inputHandler;

    private GameObject wallPoint;
    private Transform lastPoint;
    private bool isDestroyingActive;
    private bool isOnDestroying;

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
            StartMarkingWall();
        }
        else if (inputHandler.ReleaseTriggerClick())
        {
            SetWall();
        }
        else
        {
            if (isOnDestroying)
            {
                UpdateWall();
            }
        }

        if (inputHandler.TriggerCancel())
        {
            StopDestroying();
        }
    }

    public void SetupDestroyWall()
    {
        isDestroyingActive = true;

        Vector3 startWallPosition = new Vector3(raycastHandler.GetGridPositionWall().x,
                                                offsetHeightPosition,
                                                raycastHandler.GetGridPositionWall().z);
        wallPoint = Instantiate(wallPointToSpawn, startWallPosition, Quaternion.identity);
    }

    void SetGridPointPosition()
    {
        if (isDestroyingActive)
        {
            Vector3 pointGridPosition = new Vector3(raycastHandler.GetGridPositionWall().x,
                                                    offsetHeightPosition,
                                                    raycastHandler.GetGridPositionWall().z);
            wallPoint.transform.position = pointGridPosition;
        }
    }

    void StartMarkingWall()
    {
        if (isDestroyingActive)
        {
            isOnDestroying = true;

            lastPoint = new GameObject().transform;
            lastPoint.position = wallPoint.transform.position;
        }
    }

    void SetWall()
    {
        isOnDestroying = false;
    }

    void UpdateWall()
    {
        Vector3 currentWallPosition = wallPoint.transform.position;
        DestroyWall(currentWallPosition);
    }

    void DestroyWall(Vector3 currentPosition)
    {
        GameObject wallToDestroy = FindWallByLocation(currentPosition);
        Destroy(wallToDestroy);
    }

    GameObject FindWallByLocation(Vector3 location)
    {
        Wall[] walls = FindObjectsOfType<Wall>();
        foreach (Wall wall in walls)
        {
            if (wall.transform.position == location)
                return wall.gameObject;
        }

        return null;
    }

    public void StopDestroying()
    {
        isDestroyingActive = false;
        Destroy(wallPoint);
    }

    public void ResetFeature()
    {
        StopDestroying();
    }
}
