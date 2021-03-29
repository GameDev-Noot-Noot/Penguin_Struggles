using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    float rotation = 5f;
    
    public Transform text_field;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotation * Time.deltaTime, 0f);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player") 
        {
            col.GetComponent<Player_Movement>().increase_fish_count();
            text_field.GetComponent<Fish_Count>().update_fish_count();
            Destroy(gameObject);
        }
    }

    public void assign_text(Transform text_bar)
    {
        text_field = text_bar;
    }
}
