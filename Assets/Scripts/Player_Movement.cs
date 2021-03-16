using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField]
    Rect allowedArea = new Rect(-5f, -5f, 10f, 10f); // Allowed area that object can move in

    [SerializeField]
    Vector2 faceDirection = new Vector2(1, 0);

    [SerializeField, Range(0f, 15f)]
    float desiredRotation = 5f;

    [SerializeField, Range(0f, 100f)] 
    float maxSpeed = 10f; // Maximum speed

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f; // Maximum acceleration allowed

    [SerializeField, Range(0f, 1f)]
    float bounciness = 0.5f;

    [SerializeField]
    float recovery_time = 3f;

    private Vector2 previous_direction = new Vector2(0, 0);
    private Vector2 xAxis = new Vector2(1, 0);

    public GameObject health_bar;
    public GameObject scene_switcher;
    public static float fish_count;
    public static bool pack_attached;

    private Vector3 velocity;
    private Vector2 direction;
    private float health_count;
    private float previous_angle;
    private float rotation;
    private float last_reduce_hp_call = 0f;

    void Start()
    {
        fish_count = 0f;
        pack_attached = false;

        health_count = health_bar.GetComponent<Health_bar>().get_bar_size();

        float rotation = desiredRotation;
        previous_angle = calculateAngle(faceDirection, xAxis);
    }

    void Update()
    {
        if (health_count < 0f)
        {
            scene_switcher.GetComponent<Scene_switcher>().GotoMenuScene();
        }

        if (fish_count > 2f)
        {
            // Populate the pack
        }

        if (Time.time > last_reduce_hp_call + recovery_time)
        {
            if(health_count < 1f)
            {
                increase_hp(0.3f * Time.deltaTime);
            }
        }
        update_movement();
        update_rotation();
    }

    public void reduce_hp(float amount)
    {
        if (pack_attached != false)
        {
            print("send message to lower penguin count");
        }
        else
        {
            health_bar.GetComponent<Health_bar>().reduce_health_bar(amount);
            health_count -= amount;
        }

        last_reduce_hp_call = Time.time;
    }

    public void increase_hp(float amount)
    {
        health_bar.GetComponent<Health_bar>().increase_health_bar(amount);
        health_count += amount;
    }

    public void increase_fish_count()
    {
        fish_count += 1;
    }

    private void update_movement()
    {
        Vector2 playerInput; // Vector for storing player input
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f); // We are clamping the retrieved input values to unit lenght

        Vector3 desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed; // We specify our desired velocity by multiplying maximum speed by user input
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        Vector3 displacement = velocity * Time.deltaTime; // We are calculating by how much our object is gonna move based on time, so that it doesnt move too fast or too slow
        Vector3 newPosition = transform.localPosition + displacement;

        if (newPosition.x < allowedArea.xMin)
        {
            newPosition.x = allowedArea.xMin;
            velocity.x = -velocity.x * bounciness;
        }
        else if (newPosition.x > allowedArea.xMax)
        {
            newPosition.x = allowedArea.xMax;
            velocity.x = -velocity.x * bounciness;
        }

        if (newPosition.z < allowedArea.yMin)
        {
            newPosition.z = allowedArea.yMin;
            velocity.z = -velocity.z * bounciness;
        }
        else if (newPosition.z > allowedArea.yMax)
        {
            newPosition.z = allowedArea.yMax;
            velocity.z = -velocity.z * bounciness;
        }

        transform.localPosition = newPosition;
    }

    private void update_rotation()
    {
        Vector2 playerInput; // Vector for storing player input
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f); // We are clamping the retrieved input values to unit lenght

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
                    print("a");
                    faceDirection.x = Mathf.Cos((previous_angle - rotation) * Mathf.Deg2Rad);
                    faceDirection.y = Mathf.Sin((previous_angle - rotation) * Mathf.Deg2Rad);
                    transform.Rotate(0f, 0f, -rotation);
                    previous_angle -= rotation;
                }
                else
                {
                    print("b");
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

    private static float calculateAngle(Vector2 first, Vector2 second)
    {
        float angle_cos = Mathf.Acos((Vector2.Dot(first, second)) / (first.magnitude * second.magnitude));
        float result = (angle_cos * 180) / Mathf.PI;
        return result;
    }
}
