using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private float _melt_time;
    public float melt_time
    {
        get
        {
            return _melt_time;
        }
        set
        {
            _melt_time = melt_time;
        }
    }

    private float initialized_time;
    public GameObject destroyed_version;
    public GameObject gui;

    void Start()
    {
        initialized_time = Time.time;
        gui.GetComponent<gui_methods>().create_melt_bar(melt_time);
    }

    void Update()
    {
        if (Time.time - initialized_time > melt_time)
        {
            Instantiate(destroyed_version, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
