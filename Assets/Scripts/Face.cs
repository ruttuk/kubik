using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class Face : MonoBehaviour
{
    private KubeQueue KubeQueue;

    void Start()
    {
        KubeQueue = KubeQueue.Instance;
    }

    void OnMouseDown()
    {
        // if we are in append mode, tell KubeQueue to set a Kube active at the position adjacent.
        Vector3 neighborLocation = transform.position - (transform.forward * 0.5f);
        KubeQueue.EnableKubeAtLocation(neighborLocation);
    }
}
