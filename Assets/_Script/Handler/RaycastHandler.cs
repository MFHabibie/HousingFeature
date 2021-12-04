using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHandler : MonoBehaviour
{
    public bool gridHit;

    [SerializeField] private LayerMask mask;

    private InputHandler inputHandler;
    private GameObject furnitureTraced;
    private GameObject wallTraced;
    private GameObject wallDecorTraced;
    private Vector3 gridPositionWall;
    private Vector3 gridPositionFurniture;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = FindObjectOfType<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastGrid();
    }

    void RaycastGrid()
    {
        Vector2 mousePosition = inputHandler.GetMousePosition();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            gridPositionWall.x = (int)Mathf.Round(hit.point.x);
            gridPositionWall.z = (int)Mathf.Round(hit.point.z);

            gridPositionFurniture.x = hit.point.x;
            gridPositionFurniture.z = hit.point.z;

            gridHit = true;
        }
        else
        {
            gridHit = false;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Furniture")
            {
                wallTraced = null;
                wallDecorTraced = null;

                if (hit.collider.GetComponent<Furniture>())
                {
                    furnitureTraced = hit.collider.gameObject;
                }
                else if (hit.collider.GetComponentInParent<Furniture>())
                {
                    furnitureTraced = hit.collider.GetComponentInParent<Furniture>().gameObject;
                }
            }
            else if (hit.collider.tag == "Wall")
            {
                furnitureTraced = null;
                wallDecorTraced = null;

                if (hit.collider.GetComponentInParent<Wall>())
                {
                    wallTraced = hit.collider.GetComponentInParent<Wall>().gameObject;
                }
            }
            else if (hit.collider.tag == "WallDecoration")
            {
                furnitureTraced = null;
                wallTraced = null;

                if (hit.collider.GetComponentInParent<WallDecoration>())
                {
                    wallDecorTraced = hit.collider.GetComponentInParent<WallDecoration>().gameObject;
                }
            }
            else
            {
                furnitureTraced = null;
                wallTraced = null;
                wallDecorTraced = null;
            }
        }
    }

    public Vector3 GetGridPositionWall()
    {
        return gridPositionWall;
    }

    public Vector3 GetGridPositionFurniture()
    {
        return gridPositionFurniture;
    }

    public GameObject GetFurnitureObject()
    {
        return furnitureTraced;
    }
    public GameObject GetWallObject()
    {
        return wallTraced;
    }
    public GameObject GetWallDecorObject()
    {
        return wallDecorTraced;
    }
}
