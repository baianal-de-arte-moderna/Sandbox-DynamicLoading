using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTestScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // JUST DON'T
        //Camera.main.orthographicSize = Mathf.Lerp(
        //    Camera.main.orthographicSize,
        //    4 + Random.value * 2,
        //    0.1f
        //);
    }
}
