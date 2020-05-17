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
    public float perspectiveMin = 5f;
    public float perspectiveMax = 50f;

    private Camera mainCamera;
    private Camera exportCamera;
    private PointerKube pointerKube;
    private ModeManager modeManager;

    private const string exportCamTag = "ExportCamera";

    void Start()
    {
        mainCamera = Camera.main;
        pointerKube = PointerKube.Instance;
        modeManager = ModeManager.Instance;
        exportCamera = GameObject.FindGameObjectWithTag(exportCamTag).GetComponent<Camera>();
    }

    void Update()
    {
        // Reset rotation
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.rotation = Quaternion.identity;
        }

        if(Input.mouseScrollDelta.y != 0)
        {
            ZoomCameraViaScroll();
        }
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
        // If we are rotating the Kubik, disable the PointerKube, it looks clunky otherwise.
        modeManager.currentlyRotating = x != 0f || y != 0f;

        transform.Rotate(Vector3.up, -x);
        transform.Rotate(Vector3.right, -y);
    }

    void ZoomCameraViaScroll()
    {
        if(mainCamera.orthographic)
        {
            float zoomedOrthoSize = mainCamera.orthographicSize - Input.mouseScrollDelta.y * mouseScrollZoomDampen;
            mainCamera.orthographicSize = Mathf.Clamp(zoomedOrthoSize, orthographicMin, orthographicMax);
            exportCamera.orthographicSize = mainCamera.orthographicSize;
        }
        else
        {
            Vector3 zoomedPerspectivePos = mainCamera.transform.position + mainCamera.transform.forward * Input.mouseScrollDelta.y * mouseScrollZoomDampen;
            float distanceToKubik = Vector3.Distance(transform.position, zoomedPerspectivePos);
            float buffer = 0.5f;

            if(distanceToKubik >= perspectiveMin + buffer && distanceToKubik <= perspectiveMax - buffer)
            {
                mainCamera.transform.position = zoomedPerspectivePos;
            }
        }
    }
}
