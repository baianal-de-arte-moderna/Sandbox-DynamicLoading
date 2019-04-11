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
        if (GM == null) {
            GM = this;
        }
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }
}
