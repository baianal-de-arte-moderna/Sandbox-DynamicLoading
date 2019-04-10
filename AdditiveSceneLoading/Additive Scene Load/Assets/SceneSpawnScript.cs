using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSpawnScript : MonoBehaviour
{
    public int scene;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        GetComponent<Collider2D>().enabled = false;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        var rootObject = scene.GetRootGameObjects()[0];
        Debug.Log(rootObject.name);
        if (SceneManagerScript.GM.NextSceneSpot != null) {
            rootObject.transform.position = SceneManagerScript.GM.NextSceneSpot.position;
        }
        SceneManagerScript.GM.NextSceneSpot = rootObject.transform.Find("EndRef");
        GetComponent<Collider2D>().enabled = true;
    }
}
