using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_animation : MonoBehaviour
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
    public void hunting()
    {
        transform.GetComponent<Animation>().Play("Hunting");
    }

    // Update is called once per frame
    public void searching()
    {
        transform.GetComponent<Animation>().Play("Searching");
    }
}