using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    [SerializeField]
    float zoom = 2f;

    [SerializeField]
    float y_pos_cam = 20f;

    [SerializeField, Range(1, 10)]
    float max_zoom = 3f;

    [SerializeField, Range(1, 10)]
    float min_zoom = 3f;

    float initial_y_pos_cam;

    public GameObject followed;

    void Start()
    {
        initial_y_pos_cam = y_pos_cam;
    }

    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (scroll != 0)
        {
            if (initial_y_pos_cam - min_zoom < y_pos_cam + (scroll * zoom) && y_pos_cam + (scroll * zoom) < initial_y_pos_cam + max_zoom)
            {
                y_pos_cam = y_pos_cam + (scroll * zoom);
            }
        }

        Vector3 new_pos = new Vector3(0.95f * transform.position.x + 0.05f * followed.transform.position.x, y_pos_cam, 0.95f * transform.position.z + 0.05f * followed.transform.position.z);
        transform.position = new_pos;
        y_pos_cam = transform.position.y;
    }

    public void set_followed(GameObject player)
    {
        followed = player;
    }
}
