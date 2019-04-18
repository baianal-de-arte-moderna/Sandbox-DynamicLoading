using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator animator;
    public Collider2D doorSolidCollider;
    public bool active;
    [Space(10)]
    public Transform CameraBossRef;
    // Start is called before the first frame update
    void Start()
    {
        active = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (active)
        {
            animator.SetBool("Completed", SceneManagerScript.GM.isLevelCompleted);
            animator.SetTrigger("Touch");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        animator.SetTrigger("Close");
        // if we are inside the boss room disable the door
        if (transform.position.x > other.transform.position.x)
        {
            active = false;
            EnableCollider();
            SceneManagerScript.GM.playerRef.SetFixedPosition(CameraBossRef.position, true);
        }
        else
        {
            active = true;
            SceneManagerScript.GM.playerRef.ResetCameraConstraints(true, false);
        }
    }

    public void DisableCollider()
    {
        doorSolidCollider.enabled = false;
    }

    public void EnableCollider()
    {
        doorSolidCollider.enabled = true;
    }
}
