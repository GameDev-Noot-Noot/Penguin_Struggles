using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_bar : MonoBehaviour
{
    private Image health;
    private float size;

    void Start()
    {
        health = transform.GetChild(1).GetComponent<Image>();
        size = health.fillAmount;
    }

    void Update()
    {
        health.fillAmount = size;
    }

    public void reduce_health_bar(float amount)
    {
        size -= amount;
    }

    public void increase_health_bar(float amount)
    {
        size += amount;
    }

    public float get_bar_size()
    {
        return transform.GetChild(1).GetComponent<Image>().fillAmount;
    }
}