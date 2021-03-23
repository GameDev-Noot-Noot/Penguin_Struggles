using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pack : MonoBehaviour
{
    private int size;

    public GameObject child;
    private GameObject followed;
    private GameObject previous_followed;

    void Update()
    {
        if (followed == null)
        {
            reassign_followed(previous_followed);
        }
    }


    public void instantiate_child(Vector3 pos)
    {
        GameObject p = Instantiate(child, pos, Quaternion.Euler(90, 0, 0));
        p.GetComponent<Penguin>().set_followed(followed);
        p.transform.SetParent(transform);
    }

    public int get_pack_size()
    {
        return transform.childCount;
    }

    public void set_followed(GameObject e)
    {
        followed = e;
        previous_followed = e;
    }

    public void reassign_followed(GameObject e)
    {
        followed = e;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Penguin>().set_followed(followed);
        }
    }
}
