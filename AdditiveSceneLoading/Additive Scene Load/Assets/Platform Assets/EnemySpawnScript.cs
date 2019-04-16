using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnScript : MonoBehaviour
{
    // Enemies are scene numbers
    public int[] EnemyList;
    [Space(10)]
    [Range(0, 1)]
    public float spawnChance;
    // Start is called before the first frame update
    void Start()
    {
        var spawnRandom = Random.value;
        if (spawnRandom <= spawnChance && EnemyList.Length > 0) 
        {
            var enemyRandom = Random.Range(0, EnemyList.Length);
            SceneManager.sceneLoaded += OnEnemySceneLoaded;
            SceneManager.LoadSceneAsync(EnemyList[enemyRandom], LoadSceneMode.Additive);
        }
    }

    void OnEnemySceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnEnemySceneLoaded;
        var rootObject = scene.GetRootGameObjects()[0];
        rootObject.transform.position = transform.position;
        enabled = false;
    }
}
