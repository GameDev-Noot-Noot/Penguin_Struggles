using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class penguin_animations : MonoBehaviour
{
    private float stamp;

    void Start()
    {
        Animation anim = transform.GetComponent<Animation>();
        foreach (AnimationState state in anim)
        {
            state.speed = 3f;
        }
    }

    /*
    void Update()
    {

        if (Time.fixedTime % 10 == 0f)
        {
            stamp = Time.time;
        }

        if (Time.time < stamp + 5f)
        {
            walk();
        }
        else
        {
            idle();
        }

    }*/

    // Start is called before the first frame update
    public void walk()
    {
        transform.GetComponent<Animation>().Play("walk");
    }

    // Update is called once per frame
    public void idle()
    {
        transform.GetComponent<Animation>().Play("idle");
    }
}
