using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public Image overlay;

    // Update is called once per frame
    void FixedUpdate()
    {
        overlay.color = Color.Lerp(
            overlay.color,
            new Color(0, 0, 0, 200),
            0.00005f
        );
    }
}
