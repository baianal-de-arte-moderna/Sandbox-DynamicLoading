using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScrapScript : MonoBehaviour
{
    public Slider scrapBar;
    PlayerStatusScript status;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (status == null)
        {
            status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatusScript>();
            if (status != null)
                status.onScrapChange += ScrapChange;
        }
    }
    void ScrapChange(int newValue, int oldValue)
    {
        scrapBar.value = newValue;
    }
}
