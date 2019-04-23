using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBgScrollScript : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public Vector3 offset;

    void FixedUpdate()
    {
        transform.Translate(direction * speed);
    }
    void OnBecameInvisible()
    {
        transform.position += offset;
    }
}
