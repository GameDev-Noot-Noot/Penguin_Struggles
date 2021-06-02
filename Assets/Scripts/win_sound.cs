using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win_sound : MonoBehaviour
{
    private int iteration = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (iteration < 1)
        {
            FindObjectOfType<AudioManager>().Play("win");
        }

        if (iteration < 5)
        {
            iteration += 1;
        }
    }
}
