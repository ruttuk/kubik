using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class PointerKube : MonoBehaviour
{
    #region Singleton

    public static PointerKube Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    MeshRenderer m_Renderer;

    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
    }

    public void ToggleRenderer(bool enabled)
    {
        if(m_Renderer != null)
        {
            m_Renderer.enabled = enabled;
        }
    }
}
