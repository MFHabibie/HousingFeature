using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float moveSensitivity;
    [SerializeField] private float scrollSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();

        CameraZoom();
    }

    private void CameraMovement()
    {
        float xInput = Input.GetAxis("Horizontal") * moveSensitivity;
        float yInput = Input.GetAxis("Vertical") * moveSensitivity;

        transform.position = transform.position + new Vector3(xInput, 0f, yInput);
    }

    private void CameraZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;

        transform.position += transform.forward * scrollInput;
    }
}
