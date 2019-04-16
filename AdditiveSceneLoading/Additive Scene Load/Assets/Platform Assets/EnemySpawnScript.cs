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
        StartCoroutine("EnemySpawn");
    }

    IEnumerator EnemySpawn()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        var spawnRandom = Random.value;
        if (spawnRandom <= spawnChance && EnemyList.Length > 0) 
        {
            var enemyRandom = Random.Range(0, EnemyList.Length);
            SceneManager.sceneLoaded += this.OnEnemySceneLoaded;
            SceneManager.LoadSceneAsync(EnemyList[enemyRandom], LoadSceneMode.Additive);
        }
    }

    void OnEnemySceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var rootObject = scene.GetRootGameObjects()[0];
        if (rootObject.transform.position.x <= 1f)
        {
            SceneManager.sceneLoaded -= this.OnEnemySceneLoaded;
            rootObject.transform.position = transform.position;
        }
    }
}
