﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
    #endregion

    public GameObject KubeObject;
    public GameObject Kubik;

    private Kube[] AllKubes;
    private Queue<Kube> KQueue;
    private const int queueSize = 16;

    void Start()
    {
        InitializeQueue();
        SetStartingKube();
    }

    private void SetStartingKube()
    {
        Kube startingKube = KQueue.Dequeue();
        startingKube.transform.position = Vector3.zero;
        startingKube.gameObject.SetActive(true);
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
            kube.gameObject.SetActive(true);
            Debug.Log($"Dequeuing... Kubes remaining in KQueue: {KQueue.Count}");
        }
    }

    public void DisableKube(Kube kube)
    {
        KQueue.Enqueue(kube);
        Debug.Log($"Enqueuing... Kubes remaining in KQueue: {KQueue.Count}");
        kube.DeactivateKube();
    }

    public void ToggleKubeFaceColliders()
    {
        for (int i = 0; i < AllKubes.Length; i++)
        {
            if(AllKubes[i].gameObject.activeSelf)
            {
                AllKubes[i].ToggleColliders();
            }
        }
    }
}