using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationStartScript : MonoBehaviour
{
    public void AnimationEnd() 
    {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
