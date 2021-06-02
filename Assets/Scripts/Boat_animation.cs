using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat_animation : MonoBehaviour {
    public float t = 0.0f, velocity = 0.0f, rotation_limit = 5f;
    public Vector3 first_pos, second_pos;

    void Update() 
    {
        t += velocity * Mathf.Sin(Time.time);
        if (t > 1.0) { t = 1.0f; velocity *= -1; } 
        if (t < 0.0) { t = 0.0f; velocity *= -1; } 
        transform.position = (1 - t) * first_pos + t * second_pos;

        //print(transform.rotation.eulerAngles.x);

        if (velocity * 3f + transform.rotation.eulerAngles.x < rotation_limit)
        {
            transform.Rotate(velocity * 3f, 0f, 0f);
        }
        else if (velocity * 3f + transform.rotation.eulerAngles.x > 360 - rotation_limit)
        {
            transform.Rotate(velocity * 3f, 0f, 0f);
        }
    }
}