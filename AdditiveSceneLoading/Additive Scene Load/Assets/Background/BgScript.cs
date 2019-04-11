using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScript : MonoBehaviour
{
    public Transform MyReference;
    public Transform SpawnReference;

    /// <summary>
    /// OnBecameVisible is called when the renderer became visible by any camera.
    /// </summary>
    void OnBecameVisible()
    {
        #if UNITY_EDITOR
        if(Camera.current && Camera.current.name == "SceneCamera") return;
        #endif
    }

    void OnBecameInvisible()
    {
        #if UNITY_EDITOR
        if(Camera.current && Camera.current.name == "SceneCamera") return;
        #endif
        if (SpawnReference && MyReference) {
            transform.position += Vector3.right * 2 * (SpawnReference.position.x - MyReference.position.x);
        }
    }

}
