using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPlatformScript : MonoBehaviour
{
    public Transform CameraLimitPosition;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        var rootObject = scene.GetRootGameObjects()[0];
        if (SceneManagerScript.GM) 
        {
            if (SceneManagerScript.GM.NextSceneSpot != null)
            {
                rootObject.transform.position = SceneManagerScript.GM.NextSceneSpot.position;
            }
            SceneManagerScript.GM.NextSceneSpot.position = Vector3.down * 100f;
            SceneManagerScript.GM.playerRef.SetMaxCameraPos(CameraLimitPosition.position);
        }
    }
}
