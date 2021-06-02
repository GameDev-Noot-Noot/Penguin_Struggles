using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class intro_text : MonoBehaviour
{
    public float show_time = 3f;
    public float appearance_time = 2f;
    public GameObject scene_switcher;

    private Text intro;
    private int iterations;
    private int current_iteration = 0;
    private float current_time = 0f;
    private string[] lines = { "Your species is decreasing in size day by day.",
                               "As a penguin leader, you need to grow and protect your pack.",
                               "Collect food to grow your pack, and shelter it in igloo which melts with time.",
                               "However, you need to watch out for poachers around you as they are a threat to you and your pack."};
    private Vector4 color = new Vector4(255f, 255f, 255f, 0f);


    void Start()
    {
        intro = GetComponent<Text>();
        intro.text = lines[0];
        iterations = lines.Length;
    }

    void Update()
    {
       if (current_iteration < iterations)
        {
            if (current_time < appearance_time)
            {
                color.w = color.w + Time.deltaTime * 1f / appearance_time;
                intro.color = color;
            }

            if (current_time > appearance_time + show_time)
            {
                color.w = color.w - Time.deltaTime * 1f / appearance_time;
                intro.color = color;
            }

            current_time += Time.deltaTime;

            if (color.w < 0f)
            {
                current_time = 0f;
                current_iteration += 1;
                if (current_iteration < iterations) { intro.text = lines[current_iteration]; }
            }
        }
        else
        {
            scene_switcher.GetComponent<Scene_switcher>().instructions();
        }
    }
}
