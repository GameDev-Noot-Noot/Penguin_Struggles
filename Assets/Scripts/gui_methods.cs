using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gui_methods : MonoBehaviour
{
    public GameObject melt_bar;

    public void create_melt_bar(float melt_time)
    {
        Vector3 current_pos = transform.position;
        current_pos.y -= 300f;
        GameObject e = Instantiate(melt_bar, current_pos, transform.rotation);
        e.transform.SetParent(gameObject.transform);
        e.GetComponent<melt_bar>().set_melt_time(melt_time);
    }
}
