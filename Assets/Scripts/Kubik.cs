using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kubik : MonoBehaviour
{
    public float mouseRotationDampen = 0.2f;
    public float keyRotationDampen = 1.2f;

    void Update()
    {
        // Reset rotation
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.rotation = Quaternion.identity;
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

        RotateAroundOrigin(mouseSpeedX, mouseSpeedY);
    }

    void RotateByAxis()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        RotateAroundOrigin(xAxis, yAxis);
    }

    void RotateAroundOrigin(float x, float y)
    {
        transform.Rotate(transform.up, x * keyRotationDampen);
        transform.Rotate(transform.right, y * keyRotationDampen);
    }
}
