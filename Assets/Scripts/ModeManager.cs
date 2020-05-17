using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModeManager : MonoBehaviour
{
    #region Singleton

    public static ModeManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    public GameMode currentGameMode;
    public bool currentlyRotating;
    public TextMeshProUGUI modeText;
    public TextMeshProUGUI cameraModeText;
    public FlexibleColorPicker colorPicker;
    public int startingOrthoSize = 10;

    private PointerKube pointerKube;
    private KubeQueue kubeQueue;
    private Camera mainCamera;
    private Camera exportCamera;

    private Vector3 startingCameraPosition;
    private const string modePrefix = "m::";
    private const string exportCamTag = "ExportCamera";
    private bool pointerKubeActive;

    void Start()
    {
        pointerKube = PointerKube.Instance;
        kubeQueue = KubeQueue.Instance;

        currentGameMode = GameMode.Default;
        SetGameMode(GameMode.Append, true, true);

        mainCamera = Camera.main;
        startingCameraPosition = mainCamera.transform.position;
        exportCamera = GameObject.FindGameObjectWithTag(exportCamTag).GetComponent<Camera>();

        currentlyRotating = false;
        pointerKubeActive = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetGameMode(GameMode.Append, true, true);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetGameMode(GameMode.Delete, false, false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetGameMode(GameMode.Paint, true, false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetGameMode(GameMode.Sample, true, false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetGameMode(GameMode.Bgd, true, false);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            ToggleCameraMode();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePointerKube();
        }

        if(currentGameMode == GameMode.Bgd)
        {
            mainCamera.backgroundColor = colorPicker.color;
        }
    }

    void SetGameMode(GameMode mode, bool faceCollidersOn, bool pointerKubeRendered)
    {
        if(mode != currentGameMode)
        {
            modeText.text = modePrefix + mode.ToString();
            currentGameMode = mode;
            ToggleKubeFaceColliders(faceCollidersOn);
            pointerKube.gameObject.SetActive(pointerKubeRendered);
        }
    }

    void ToggleKubeFaceColliders(bool faceCollidersOn)
    {
        Debug.Log($"Current Game Mode: {currentGameMode}");
        kubeQueue.ToggleKubeFaceColliders(faceCollidersOn);
    }

    void ToggleCameraMode()
    {
        string cameraModeString;

        mainCamera.orthographic = !mainCamera.orthographic;
        exportCamera.orthographic = mainCamera.orthographic;

        if (mainCamera.orthographic)
        {
            mainCamera.transform.position = startingCameraPosition;
            mainCamera.orthographicSize = startingOrthoSize;
            cameraModeString = "c::Ortho";
        }
        else
        {
            cameraModeString = "c::Persp";
        }

        cameraModeText.text = cameraModeString;
    }

    void TogglePointerKube()
    {
        pointerKubeActive = !pointerKubeActive;
        pointerKube.gameObject.SetActive(pointerKubeActive);
    }
}

public enum GameMode
{
    Default,
    Append,
    Delete,
    Paint,
    Sample,
    Bgd
}
