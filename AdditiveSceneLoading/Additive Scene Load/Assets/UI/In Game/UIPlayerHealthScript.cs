using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealthScript : MonoBehaviour
{
    public Slider healthBar;
    PlayerStatusScript status;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (status == null)
        {
            status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatusScript>();
            if (status != null)
                status.onHealthChange += HealthChange;
        }
    }
    void HealthChange(int newValue)
    {
        healthBar.value = newValue;
    }
}
