using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTitleScript : MonoBehaviour
{
    public float speed;
    public Vector2 fuzz;
    Vector3 target = Vector3.zero;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            target,
            speed
        );

        target = new Vector3(
            Random.Range(-fuzz.x, fuzz.x),
            Random.Range(-fuzz.y, fuzz.y),
            0f
        );
    }
}
