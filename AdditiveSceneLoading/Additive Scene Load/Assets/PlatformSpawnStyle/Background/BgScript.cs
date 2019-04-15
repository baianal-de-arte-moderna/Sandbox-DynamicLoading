using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScript : MonoBehaviour
{
    public Transform MyReference;
    public Transform SpawnReference;

    float distance;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        distance = Mathf.Abs(SpawnReference.position.x - MyReference.position.x) * 2;
    }

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
        try
        {
            if (Camera.main.transform.position.x > transform.position.x) {
                transform.position += Vector3.right * distance;
            } else {
                transform.position += Vector3.left * distance;
            }
        }
        catch (System.NullReferenceException) 
        {
            return;
        }
    }

}
