﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSpawnScript : MonoBehaviour
{
    const int sceneCount = 12;
    int nextScene;
    Collider2D collid;
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        nextScene = Mathf.FloorToInt(Random.value * sceneCount) + 2;
        collid = GetComponent<Collider2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        collid.enabled = false;
        SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
    }

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
            SceneManagerScript.GM.NextSceneSpot = rootObject.transform.Find("EndRef");
            GetComponent<Collider2D>().enabled = true;
        }
    }
}