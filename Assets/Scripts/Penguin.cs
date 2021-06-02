using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : MonoBehaviour
{
    [SerializeField]
    public float walking_speed = 0.95f;

    [SerializeField, Range(0f, 15f)]
    float desiredRotation = 5f;

    [SerializeField]
    Vector2 faceDirection = new Vector2(1, 0);

    private int penguin_id;
    private GameObject followed;
    private Vector2 direction;
    private Vector2 previous_direction = new Vector2(0, 0);
    private Vector2 xAxis = new Vector2(1, 0);
    private float previous_angle;
    private float rotation;
    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        previous_angle = calculateAngle(faceDirection, xAxis);
        //offset = Random.Range(-0.3f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (followed != null)
        {
            Vector2 dir = new Vector2(followed.transform.position.x - transform.position.x, followed.transform.position.z - transform.position.z);
            dir.Normalize();
            update_rotation(dir);
            Vector3 new_pos = (walking_speed - Time.deltaTime) * transform.position + (1f - (walking_speed - Time.deltaTime)) * followed.transform.position;
            transform.position = new_pos;

            Vector2 dist = new Vector2(followed.transform.position.x - transform.position.x, followed.transform.position.y - transform.position.y);
            if (dist.magnitude < 0.3f)
            {
                transform.GetChild(0).GetComponent<penguin_animations>().idle();
            }
            else
            {
                transform.GetChild(0).GetComponent<penguin_animations>().walk();
            }
        }
    }

    private void update_rotation(Vector2 playerInput)
    {
        direction = playerInput;
        direction.Normalize();

        if (direction != new Vector2(0, 0))
        {
            if (faceDirection != direction)
            {
                float checkAngle = calculateAngle(faceDirection, direction);
                Vector2 checkVector = new Vector2(Mathf.Cos((previous_angle - checkAngle) * Mathf.Deg2Rad), Mathf.Sin((previous_angle - checkAngle) * Mathf.Deg2Rad));

                if (checkAngle < rotation)
                {
                    rotation = checkAngle;
                }

                if (checkVector == direction)
                {
                    faceDirection.x = Mathf.Cos((previous_angle - rotation) * Mathf.Deg2Rad);
                    faceDirection.y = Mathf.Sin((previous_angle - rotation) * Mathf.Deg2Rad);
                    transform.Rotate(0f, 0f, -rotation);
                    previous_angle -= rotation;
                }
                else
                {
                    faceDirection.x = Mathf.Cos((previous_angle + rotation) * Mathf.Deg2Rad);
                    faceDirection.y = Mathf.Sin((previous_angle + rotation) * Mathf.Deg2Rad);
                    transform.Rotate(0f, 0f, rotation);
                    previous_angle += rotation;
                }
            }
            if (previous_angle == 360 || previous_angle == -360)
            {
                previous_angle = 0;
            }

            rotation = desiredRotation;
        }
    }

    public void set_id(int i)
    {
        penguin_id = i;
    }

    private static float calculateAngle(Vector2 first, Vector2 second)
    {
        float angle_cos = Mathf.Acos((Vector2.Dot(first, second)) / (first.magnitude * second.magnitude));
        float result = (angle_cos * 180) / Mathf.PI;
        return result;
    }

    public void set_followed(GameObject p)
    {
        followed = p;
    }

    public void destroy_self()
    {
        Destroy(gameObject);
    }
}
