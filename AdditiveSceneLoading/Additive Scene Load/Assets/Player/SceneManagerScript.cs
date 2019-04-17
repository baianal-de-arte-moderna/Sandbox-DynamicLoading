using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]
    int[] EnemyList;
    [Range(0f, 1f)]
    [SerializeField]
    float[] SpawnRate;
    float totalSpawnRate;
    public GameStyles GameStyle;
    // Start is called before the first frame update
    void Start()
    {
        totalSpawnRate = SpawnRate.Sum();

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
        }
        else
        {
            // Level
            SceneManager.LoadScene(16, LoadSceneMode.Additive);
        }
    }

    // Randomizes one enemy from the list based on their spawn rates
    public int GetEnemy()
    {
        var enemyRandom = Random.Range(0f, totalSpawnRate);
        for(var i = 0; i < EnemyList.Length; i++) 
        {
            enemyRandom -= SpawnRate[i];
            if (enemyRandom <= 0f)
            {
                return EnemyList[i];
            }
        }
        return EnemyList[0];
    }
}
