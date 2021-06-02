using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pack : MonoBehaviour
{
    private int size;

    private Text penguin_count_text;
    public GameObject child;
    private GameObject followed;
    private GameObject previous_followed;

    void Start()
    {
        //penguin_count_text.text = "" + transform.childCount;
    }

    void Update()
    {
        if (followed == null)
        {
            reassign_followed(previous_followed);
        }

        penguin_count_text.text = "" + transform.childCount + "/ 10";
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

    public Transform get_followed()
    {
        if (get_pack_size() > 0 && followed != null)
        {
            return followed.transform;
        }
        else
        {
            return null;
        }
    }

    public void destroy_last_child()
    {
        if(get_pack_size() > 0)
        {
            FindObjectOfType<AudioManager>().Play("Penguin_down");
            transform.GetChild(transform.childCount - 1).GetComponent<Penguin>().destroy_self();
        }
    }
    
    public void set_text(GameObject reference)
    {
        penguin_count_text = reference.GetComponent<Text>();
    }
}
