using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    private int fps = 0;
    private float ellapsed_time = 0f;

    // Update is called once per frame
    void Update()
    {
        ellapsed_time += Time.deltaTime;
        fps = fps + 1;

        if (ellapsed_time > 1f)
        {
            Text e = GetComponent<Text>();
            e.text = "FPS: " + fps;
            fps = 0;
            ellapsed_time = 0f;
        }
    }
}
