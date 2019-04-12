using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRefScript : MonoBehaviour
{
    public Transform player;
    public Vector2 offset;
    public Vector2 scale;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 
        (
            (player.position.x + offset.x) * scale.x,
            (player.position.y + offset.y) * scale.y,
            player.position.z
        );
    }
}
