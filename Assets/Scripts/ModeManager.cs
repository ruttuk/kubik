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
    public TextMeshProUGUI modeText;

    private KubeQueue kubeQueue;

    private const string modePrefix = "m::";

    void Start()
    {
        kubeQueue = KubeQueue.Instance;
        currentGameMode = GameMode.Default;
        SetGameMode(GameMode.Append, true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetGameMode(GameMode.Append, true);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetGameMode(GameMode.Delete, false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetGameMode(GameMode.Paint, true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetGameMode(GameMode.Sample, true);
        }
    }

    void SetGameMode(GameMode mode, bool faceCollidersOn)
    {
        if(mode != currentGameMode)
        {
            modeText.text = modePrefix + mode.ToString();
            currentGameMode = mode;
            ToggleKubeFaceColliders(faceCollidersOn);
        }
    }

    void ToggleKubeFaceColliders(bool faceCollidersOn)
    {
        Debug.Log($"Current Game Mode: {currentGameMode}");
        kubeQueue.ToggleKubeFaceColliders(faceCollidersOn);
    }
}

public enum GameMode
{
    Default,
    Append,
    Delete,
    Paint,
    Sample
}
