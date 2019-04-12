using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRefScript : MonoBehaviour
{
    public Transform player;
    public Vector2 offset;
    public Vector2 scale;
    public SpriteRenderer charAnimator;
    Vector3 minPos;
    float targetX;
    float baseX;

    void Start()
    {
        minPos = transform.position;
        baseX = offset.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (charAnimator.flipX) 
        {
            targetX = -baseX;
        }
        else
        {
            targetX = baseX;
        }
        offset.x = Mathf.Lerp(offset.x, targetX, 0.015f);
        
        var newPos = new Vector3 
        (
            (player.position.x + offset.x) * scale.x,
            (player.position.y + offset.y) * scale.y,
            player.position.z
        );
        if (newPos.x > minPos.x)
            transform.position = newPos;
    }
}
