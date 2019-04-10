using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public int scene;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Control
        if (Input.GetKey(KeyCode.D)) {
            transform.position += Vector3.right;
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.position += Vector3.left;
        }
    }
}
