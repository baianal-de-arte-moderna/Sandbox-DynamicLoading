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
    public PlayerRefScript playerRef;
    public int PlatformEnd;

    [SerializeField]
    int[] EnemyList;

    [Range(0f, 1f)]
    [SerializeField]
    float[] SpawnRate;
    float totalSpawnRate;

    [SerializeField]
    int[] BossList;
    int chosenBoss;
    bool finalized;
    public GameStyles GameStyle;
    BossScript boss;
    [HideInInspector]
    public BossScript Boss
    {
        get
        {
            return boss;
        }
    }
    public bool isLevelCompleted
    {
        get
        {
            return finalized;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        totalSpawnRate = SpawnRate.Sum();
        finalized = false;
        chosenBoss = -1;

        if (GM == null) 
        {
            GM = this;
        }
        // Animation
        SceneManager.LoadScene(15, LoadSceneMode.Additive);
        // UI
        SceneManager.LoadScene(18, LoadSceneMode.Additive);
        // Ready Animation
        SceneManager.LoadScene(21, LoadSceneMode.Additive);
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

    public void FinalizePlatformSpawning()
    {
        if (!finalized)
        {
            finalized = true;
            SceneManager.LoadSceneAsync(PlatformEnd, LoadSceneMode.Additive);
            Invoke("LoadBossScene", 2f);
        }
    }

    public void LoadBossScene()
    {
        if (chosenBoss < 0)
        {
            chosenBoss = BossList[Random.Range(0, BossList.Length)];
            SceneManager.sceneLoaded += SetBossScript;
            SceneManager.LoadSceneAsync(chosenBoss, LoadSceneMode.Additive);
        }
    }
    public void SetBossScript(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= SetBossScript;
        var rootObject = scene.GetRootGameObjects()[0];
        boss = rootObject.GetComponentInChildren<BossScript>();
        boss.onFinishDeath += EndLevel;
    }
    public void StartBossSequence()
    {
        if (boss != null)
        {
            boss.StartPresentation();
        }
    }

    public void EndLevel()
    {
        // TODO: Call end-level UI and Score
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
