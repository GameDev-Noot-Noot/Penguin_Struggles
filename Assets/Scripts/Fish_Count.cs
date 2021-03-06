using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish_Count : MonoBehaviour
{
    private Text fish_count_text;
    private int count;

    void Start()
    {
        fish_count_text = GetComponent<Text>();
        count = 0;
    }

    void Update()
    {
        fish_count_text.text = "Fish Count: " + count;
    }

    public void update_fish_count()
    {
        count += 1;
    }
}