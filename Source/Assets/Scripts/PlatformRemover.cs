using UnityEngine;
using System.Collections;

/// <summary>
/// Used to remove platforms and characters that below user screen.
/// </summary>
public class PlatformRemover : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Platform p = other.collider.GetComponent<Platform>();
        Character c = other.collider.GetComponent<Character>();        
        if (p != null)
        {
            p.Release();
        }
        else if (c != null)
        {
            c.gameObject.BroadcastMessage("Kill", SendMessageOptions.DontRequireReceiver);
        }

    }
}
