using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igloo_parts : MonoBehaviour
{
    private float initialized_time;

    void Start()
    {
        initialized_time = Time.time;
    }

    void Update()
    {
        if(Time.time - initialized_time > 5f)
        {
            Destroy(gameObject);
        }
    }
}
