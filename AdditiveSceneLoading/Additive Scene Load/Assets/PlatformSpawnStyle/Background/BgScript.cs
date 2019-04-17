using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScript : MonoBehaviour
{
    public Transform[] BgList;

    Camera mainCamera;
    Vector3 movement;

    float bgSize;

    void Start()
    {
        int bgCount = BgList.Length;
        bgSize = Mathf.Abs(BgList[0].position.x - BgList[1].position.x);
        mainCamera = Camera.main;
        movement = Vector3.right * bgSize;
    }

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
            if (mainCamera.transform.position.x > transform.position.x) {
                foreach(var bg in BgList)
                    bg.position += movement;
            } else {
                foreach(var bg in BgList)
                    bg.position -= movement;
            }
        }
        catch (UnityEngine.MissingReferenceException) 
        {
            return;
        }
    }

}
