using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextDestroyScript : MonoBehaviour
{
    
    public void Unload()
    {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
