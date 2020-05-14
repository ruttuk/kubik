using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public enum GameMode
    {
        Append,
        Delete,
        Paint
    }

    public GameMode currentGameMode;

    private KubeQueue kubeQueue;

    void Start()
    {
        kubeQueue = KubeQueue.Instance;
        currentGameMode = GameMode.Append;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && currentGameMode != GameMode.Append)
        {
            currentGameMode = GameMode.Append;
            ToggleKubeFaceColliders();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && currentGameMode != GameMode.Delete)
        {
            currentGameMode = GameMode.Delete;
            ToggleKubeFaceColliders();
        }
    }

    /***
     * 
     * All face colliders must be switched off, kube colliders switched on.
     * 
     ***/
    void ToggleKubeFaceColliders()
    {
        Debug.Log($"Current Game Mode: {currentGameMode}");
        kubeQueue.ToggleKubeFaceColliders();
    }
}
