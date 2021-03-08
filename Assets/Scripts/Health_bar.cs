using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_bar : MonoBehaviour
{
    private Image health;
    private int size;

    void Start()
    {
        health = GetComponent<Image>();
        size = 200;
    }

    void Update()
    {
        health.rectTransform.sizeDelta = new Vector2(size, 40);
    }

    public void reduce_health_bar(int amount)
    {
        size -= amount;
    }

    public Vector2 get_bar_size()
    {
        return health.rectTransform.sizeDelta;
    }
}