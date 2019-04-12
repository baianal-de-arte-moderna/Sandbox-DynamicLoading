using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript GM;
    public Transform NextSceneSpot;
    // Start is called before the first frame update
    void Start()
    {
        if (GM == null) 
        {
            GM = this;
        }
        // BG
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        // Platform
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        // Animation
        SceneManager.LoadScene(15, LoadSceneMode.Additive);
    }
}
