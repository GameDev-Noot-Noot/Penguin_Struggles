using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction_screen : MonoBehaviour
{
    public float transition_time = 1f;

    private float desired_position = 0f;
    private RectTransform element;
    private float ellapsed_time = 0f;
    private bool clicked = false;

    void Start()
    {
        element = GetComponent<RectTransform>();
        desired_position = element.anchoredPosition.x;
        ellapsed_time = transition_time + 1f;
    }

    void Update()
    {

        if (ellapsed_time < transition_time)
        {
            if (element.anchoredPosition.x > desired_position)
            {
                element.anchoredPosition = new Vector2(element.anchoredPosition.x - Time.deltaTime * 1920f / transition_time, element.anchoredPosition.y);
            }
            else
            {
                element.anchoredPosition = new Vector2(element.anchoredPosition.x + Time.deltaTime * 1920f / transition_time, element.anchoredPosition.y);
            }
                    
            ellapsed_time += Time.deltaTime;
        }

        if (clicked == true)
        {
            ellapsed_time = 0f;
            clicked = false;
        }

        /*
        if (element.anchoredPosition.x != desired_position)
        {
            if (element.anchoredPosition.x > desired_position)
            {
                element.anchoredPosition = new Vector2(element.anchoredPosition.x - (Mathf.Round(1000f * Time.fixedDeltaTime)), element.anchoredPosition.y);
            }
            else
            {
                element.anchoredPosition = new Vector2(element.anchoredPosition.x + (Mathf.Round(1000f * Time.fixedDeltaTime)), element.anchoredPosition.y);
            }
        }

        print((Mathf.Round(1000f * Time.fixedDeltaTime)));
        */
    }
    
    public void move_right()
    {
        desired_position -= 1920f;
        clicked = true;
    }

    public void move_left()
    {
        desired_position += 1920f;
        clicked = true;
    }
}
