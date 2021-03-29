using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gui_methods : MonoBehaviour
{
    public GameObject melt_bar;
    public GameObject pause_menu;
    public GameObject pause_menu_instance;

    public void create_melt_bar(float melt_time)
    {
        Vector3 current_pos = transform.position;
        current_pos.y -= 300f;
        GameObject e = Instantiate(melt_bar, current_pos, transform.rotation);
        e.transform.SetParent(gameObject.transform);
        e.GetComponent<melt_bar>().set_melt_time(melt_time);
    }

    public Transform get_health_bar()
    {
        return transform.GetChild(1);
    }

    public Transform get_fish_counter()
    {
        return transform.GetChild(2);
    }

    public void create_pause_screen()
    {
        pause_menu_instance = Instantiate(pause_menu, transform.position, transform.rotation);
        pause_menu_instance.transform.SetParent(gameObject.transform);
        Time.timeScale = 0f;
    }

    public void remove_pause_screen()
    {
        Time.timeScale = 1f;
        Destroy(pause_menu_instance);
    }
}
