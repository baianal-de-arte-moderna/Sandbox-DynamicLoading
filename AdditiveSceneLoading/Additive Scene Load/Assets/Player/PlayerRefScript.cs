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
    Vector3 maxPos;
    Vector3 originalMinPos;
    Vector3 originalMaxPos;
    float targetX;
    float baseX;
    bool isFixed;
    bool isSmooth;
    Vector3 fixedTarget;
    Vector3 fixedVelocity;

    void Start()
    {
        minPos = transform.position;
        originalMinPos = minPos;
        maxPos = Vector3.right * int.MaxValue;
        originalMaxPos = maxPos;

        baseX = offset.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFixed)
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
            if (newPos.x < minPos.x)
                newPos.x = minPos.x;
            if (newPos.x > maxPos.x)
                newPos.x = maxPos.x;
            transform.position = newPos;
        }
        else if(isSmooth)
        {
            transform.position += fixedVelocity;
            if (Vector3.Distance(transform.position, fixedTarget) <= 0.1f) {
                isSmooth = false;
                transform.position = fixedTarget;
            }
        }
    }

    public void SetMaxCameraPos(Vector3 newMaxPos)
    {
        isFixed = false;
        maxPos = newMaxPos;
    }

    public void SetMinCameraPos(Vector3 newMinPos)
    {
        isFixed = false;
        minPos = newMinPos;
    }

    public void ResetCameraConstraints(bool resetMinPos, bool resetMaxPos)
    {
        isFixed = false;
        if (resetMinPos) minPos = originalMinPos;
        if (resetMaxPos) maxPos = originalMaxPos;
    }

    public void SetFixedPosition(Vector3 position, bool smoothTransition)
    {
        isFixed = true;
        isSmooth = smoothTransition;
        if (!smoothTransition) {
            transform.position = position;
            maxPos = position;
            minPos = position;
        }
        else
        {
            fixedTarget = position;
            maxPos = position;
            minPos = position;

            fixedVelocity = Vector3.Lerp(
                transform.position,
                fixedTarget,
                0.4f * Time.deltaTime
            ) - transform.position;
        }
    }
}
