using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Kube : MonoBehaviour
{
    private KubeQueue KubeQueue;
    private BoxCollider kubeCollider;
    private Face[] faces;

    void Start()
    {
        KubeQueue = KubeQueue.Instance;
        kubeCollider = GetComponent<BoxCollider>();
        kubeCollider.enabled = false;
        faces = GetComponentsInChildren<Face>();
    }

    void OnMouseDown()
    {
        KubeQueue.DisableKube(this);
    }

    public void SetKubeColor(Color color)
    {
        faces = GetComponentsInChildren<Face>();

        for (int i = 0; i < faces.Length; i++)
        {
            faces[i].GetComponent<MeshRenderer>().material.color = color;
        }
    }

    public void DeactivateKube()
    {
        kubeCollider.enabled = false;
        ToggleFaceColliders(true);
        gameObject.SetActive(false);
    }

    public void ToggleColliders(bool faceCollidersOn)
    {
        Debug.Log("Toggling Colliders...");
        kubeCollider.enabled = !faceCollidersOn;
        ToggleFaceColliders(faceCollidersOn);
    }

    private void ToggleFaceColliders(bool toggle)
    {
        MeshCollider faceCollider;

        for (int i = 0; i < faces.Length; i++)
        {
            faceCollider = faces[i].GetComponent<MeshCollider>();
            faceCollider.enabled = toggle;
        }
    }
}
