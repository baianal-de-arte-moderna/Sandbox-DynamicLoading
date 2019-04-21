using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScrapScript : MonoBehaviour
{
    public Slider scrapBar;
    PlayerStatusScript status;
    bool hasBoss;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (status == null)
        {
            status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatusScript>();
            if (status != null)
                status.onScrapChange += ScrapChange;
        }
        if (!hasBoss)
        {
            if (SceneManagerScript.GM.Boss != null)
            {
                hasBoss = true;
                SceneManagerScript.GM.Boss.onBossHealthChange += ScrapChange;
            }
        }
    }
    void ScrapChange(int newValue, int oldValue)
    {
        scrapBar.value = newValue;
    }
}
