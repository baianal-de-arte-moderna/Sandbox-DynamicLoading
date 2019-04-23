using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuItemScript : MonoBehaviour
{
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void SetSelected(bool selected)
    {
        anim.SetBool("Selected", selected);
    }

    public void Activate()
    {
        SceneManager.LoadScene(25);
    }
}
