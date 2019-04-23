using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public MenuItemScript[] menuItems;
    int selected;
    int Selected
    {
        get
        {
            return selected;
        }
        set
        {
            selected = value % menuItems.Length;
            if (selected < 0) selected += menuItems.Length;
            for (var i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].SetSelected(selected == i);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        selected = -1;
        Invoke("Activate", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Selected += 1;
        } else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Selected -= 1;
        } else if (Input.GetKeyDown(KeyCode.Return))
        {
            menuItems[selected].Activate();
        }
    }

    void Activate()
    {
        Selected = 0;
    }
}
