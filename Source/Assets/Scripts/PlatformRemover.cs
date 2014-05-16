using UnityEngine;
using System.Collections;

public class PlatformRemover : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Platform p = other.collider.GetComponent<Platform>();
        if (p != null)
        {
            p.Release();
        }
    }
}
