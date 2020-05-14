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
        faces = GetComponentsInChildren<Face>();
        kubeCollider.enabled = false;
    }

    void OnMouseDown()
    {
        KubeQueue.DisableKube(this);
    }

    public void DeactivateKube()
    {
        kubeCollider.enabled = false;
        ToggleFaceColliders(true);
        gameObject.SetActive(false);
    }

    public void ToggleColliders()
    {
        Debug.Log("Toggling Colliders...");
        kubeCollider.enabled = !kubeCollider.enabled;
        ToggleFaceColliders(!kubeCollider.enabled);
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
