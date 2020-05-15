using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlexibleColorPicker))]
public class ColorCoordinator : MonoBehaviour
{
    public Color defaultKubeColor;
    FlexibleColorPicker m_ColorPicker;

    void Start()
    {
        m_ColorPicker = GetComponent<FlexibleColorPicker>();
        m_ColorPicker.color = defaultKubeColor;
    }

    public Color GetCurrentColor()
    {
        return m_ColorPicker.color;
    }
}
