﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public int scene;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Control
        if (Input.GetKey(KeyCode.D)) {
            transform.position += Vector3.right * speed;
            animator.SetBool("Running", true);
        } else if (Input.GetKey(KeyCode.A)) {
            transform.position += Vector3.left * speed;
            animator.SetBool("Running", true);
        } else {
            animator.SetBool("Running", false);
        }
    }
}
