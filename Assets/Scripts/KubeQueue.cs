using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KubeQueue : MonoBehaviour
{
    #region Singleton

    public static KubeQueue Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        InitializeQueue();
    }
    #endregion

    public GameObject KubeObject;
    public GameObject Kubik;
    public FlexibleColorPicker colorPicker;
    public TextMeshProUGUI remainingKubesText;

    private Kube[] AllKubes;
    private Queue<Kube> KQueue;

    private const int queueSize = 64;
    public const string pointerKubeTag = "PointerKube";

    void Start()
    {
        SetStartingKube();
        SetRemainingKubesText();
    }

    private void SetStartingKube()
    {
        Kube startingKube = KQueue.Dequeue();
        startingKube.transform.position = Vector3.zero;
        startingKube.gameObject.SetActive(true);
        startingKube.SetKubeColor(colorPicker.startingColor);
    }

    private void InitializeQueue()
    {
        KQueue = new Queue<Kube>();
        AllKubes = new Kube[queueSize];

        Kube kube;

        for(int i = 0; i < queueSize; i++)
        {
            GameObject kubeObj = Instantiate(KubeObject, Vector3.zero, Quaternion.identity, Kubik.transform);
            kube = kubeObj.GetComponent<Kube>();
            kubeObj.SetActive(false);

            KQueue.Enqueue(kube);
            AllKubes[i] = kube;
        }
    }

    public void EnableKubeAtLocation(Vector3 location)
    {
        if(KQueue.Count > 0)
        {
            Kube kube = KQueue.Dequeue();
            kube.transform.position = location;
            kube.SetKubeColor(colorPicker.color);
            kube.gameObject.SetActive(true);
            Debug.Log($"Dequeuing with color {colorPicker.color}... Kubes remaining in KQueue: {KQueue.Count}");
            SetRemainingKubesText();
        }
    }

    public void DisableKube(Kube kube)
    {
        KQueue.Enqueue(kube);
        Debug.Log($"Enqueuing... Kubes remaining in KQueue: {KQueue.Count}");
        kube.DeactivateKube();
        SetRemainingKubesText();
    }

    public void ToggleKubeFaceColliders(bool faceCollidersOn)
    {
        for (int i = 0; i < AllKubes.Length; i++)
        {
            if(AllKubes[i].gameObject.activeSelf)
            {
                AllKubes[i].ToggleColliders(faceCollidersOn);
            }
        }
    }

    void SetRemainingKubesText()
    {
        remainingKubesText.text = KQueue.Count.ToString() + "/" + queueSize.ToString();
    }
}
