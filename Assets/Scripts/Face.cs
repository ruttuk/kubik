﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class Face : MonoBehaviour
{
    private KubeQueue KubeQueue;
    private ModeManager ModeManager;
    private FlexibleColorPicker colorPicker;

    MeshRenderer m_Renderer;

    void Start()
    {
        KubeQueue = KubeQueue.Instance;
        ModeManager = ModeManager.Instance;
        colorPicker = FindObjectOfType<FlexibleColorPicker>();

        m_Renderer = GetComponent<MeshRenderer>();
    }

    void OnMouseDown()
    {
        // Ensure we don't accidentally place a block while rotating.
        if(!Input.GetKey(KeyCode.LeftShift))
        {
            switch(ModeManager.currentGameMode)
            {
                case GameMode.Append:
                    // if we are in append mode, tell KubeQueue to set a Kube active at the position adjacent.
                    Vector3 neighborLocation = transform.position - (transform.forward * 0.5f);
                    KubeQueue.EnableKubeAtLocation(neighborLocation);
                    break;
                case GameMode.Paint:
                    // set color
                    m_Renderer.material.color = colorPicker.color;
                    break;
                case GameMode.Sample:
                    // get color and set color picker
                    colorPicker.color = m_Renderer.material.color;
                    break;
            }

        }
    }
}
