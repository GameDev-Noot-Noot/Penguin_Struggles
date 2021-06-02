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
        fish_count_text.text = "" + count;
    }

    public void update_fish_count()
    {
        if (count + 1 >= 3)
        {
            count = 0;
        }
        else
        {
            count += 1;
        }
    }
}