using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kubik : MonoBehaviour
{
    public float mouseRotationDampen = 0.2f;
    public float keyRotationDampen = 3.5f;
    public float mouseScrollZoomDampen = 2f;
    public float orthographicMax = 30f;
    public float orthographicMin = 3f;


    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Reset rotation
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.rotation = Quaternion.identity;
        }

        ZoomCameraViaScroll();
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(0))
        {
            RotateViaMouse();
        }

        RotateByAxis();
    }

    void RotateViaMouse()
    {
        float mouseSpeedX = Input.GetAxis("Mouse X") / Time.deltaTime;
        float mouseSpeedY = Input.GetAxis("Mouse Y") / Time.deltaTime;

        RotateAroundOrigin(mouseSpeedX * mouseRotationDampen, mouseSpeedY * mouseRotationDampen);
    }

    void RotateByAxis()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        RotateAroundOrigin(xAxis * keyRotationDampen, yAxis * keyRotationDampen);
    }

    void RotateAroundOrigin(float x, float y)
    {
        transform.Rotate(Vector3.up, -x);
        transform.Rotate(Vector3.right, -y);
    }

    void ZoomCameraViaScroll()
    {
        if(mainCamera.orthographic && Input.mouseScrollDelta.y != 0)
        {
            float zoomedOrthoSize = mainCamera.orthographicSize - Input.mouseScrollDelta.y * mouseScrollZoomDampen;
            mainCamera.orthographicSize = Mathf.Clamp(zoomedOrthoSize, orthographicMin, orthographicMax);
        }
    }
}
