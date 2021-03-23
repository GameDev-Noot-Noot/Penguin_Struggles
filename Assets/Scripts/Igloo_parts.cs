using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igloo_parts : MonoBehaviour
{
    private float initialized_time;
    private float size = 10f;

    void Start()
    {
        initialized_time = Time.time;
    }

    void Update()
    {
        size -= Time.deltaTime * 10 / 5f;
        transform.localScale = new Vector3(size, size, size);

        if (Time.time - initialized_time > 5f)
        {
            Destroy(gameObject);
        }
    }
}
