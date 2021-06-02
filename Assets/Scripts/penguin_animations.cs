using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class penguin_animations : MonoBehaviour
{
    //private float stamp;
    public float animation_speed = 3f;

    void Start()
    {
        Animation anim = transform.GetComponent<Animation>();
        foreach (AnimationState state in anim)
        {
            state.speed = animation_speed;
        }
    }

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
