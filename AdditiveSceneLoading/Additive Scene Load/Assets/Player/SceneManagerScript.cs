using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public enum GameStyles {
        PLATFORM_STYLE,
        LEVEL_STYLE
    }
    public static SceneManagerScript GM;
    public Transform NextSceneSpot;
    public GameStyles GameStyle;
    // Start is called before the first frame update
    void Start()
    {
        if (GM == null) 
        {
            GM = this;
        }
        // Animation
        SceneManager.LoadScene(15, LoadSceneMode.Additive);
        // UI
        SceneManager.LoadScene(18, LoadSceneMode.Additive);
        if (GameStyle == GameStyles.PLATFORM_STYLE)
        {
            // BG
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            // Platform
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            // First Enemy
            SceneManager.LoadScene(17, LoadSceneMode.Additive);
        }
        else
        {
            // Level
            SceneManager.LoadScene(16, LoadSceneMode.Additive);
        }
    }
}
