using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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
    float walking_speed_enemy = 0.97f;

    [SerializeField]
    public int segments = 50;

    [SerializeField]
    public float damage = 0.3f;

    private List<GameObject> ObjectsInRange = new List<GameObject>();
    LineRenderer line;

    private Vector3 velocity;
    private Vector2 direction;
    private Vector2 previous_direction = new Vector2(0, 0);
    private Vector2 xAxis = new Vector2(1, 0);
    private float previous_angle;
    private float rotation;
    private Vector2 random_dir;

    float vision_field;
    string state;


    void Start()
    {
        /* Retrieving the collider and setting its radius to vision field*/
        SphereCollider object_collider = GetComponent<SphereCollider>();
        vision_field = object_collider.radius;

        /* Retrieving the line rendere and giving it a new material, and setting the default color to yellow*/
        line = gameObject.GetComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        Color c1 = new Color(1f, 1f, 0f, 1); 
        line.SetColors(c1, c1); //setting the vision field circle to yellow  

        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;

        state = "search";
        random_dir = new Vector2(0, 0);
        previous_angle = calculateAngle(faceDirection, xAxis);
    }

    void Update()
    {
        draw_vision_circle();

        if (ObjectsInRange.Count != 0) { state = "follow"; } //making sure the state of Enemy object is changed if there is an object in the vision field
        else { state = "search"; }


        if (state == "search") 
        {
            if(Time.fixedTime % 2f == 0f)
            {

                random_dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                random_dir.Normalize();
            }
            searching(random_dir); 
        } //calling appropriate methods when Enemy's state is changed
        else 
        { 
            following(Time.deltaTime);
            ObjectsInRange[0].GetComponent<Player_Movement>().reduce_hp(damage*Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider col) //method called when there is a Player object in the spehre collider radius of the Enemy object
    {
        if (col.gameObject.tag == "Player") //if there is, the vision field changes color to red
        {
            ObjectsInRange.Add(col.gameObject);
            Color c2 = new Color(1f, 0f, 0f, 1);
            line.SetColors(c2, c2);
            transform.GetChild(0).GetComponent<enemy_animation>().hunting();
        }
    }

    public void OnTriggerExit(Collider col) //method called when there is no object in the spehre collider radius of the Enemy object

    {
        if (col.gameObject.tag == "Player")
        {
            ObjectsInRange.Remove(col.gameObject); // changes the vision field circle to color yellow again
            Color c3 = new Color(1f, 1f, 0f, 1);
            line.SetColors(c3, c3);
            transform.GetChild(0).GetComponent<enemy_animation>().searching();
        }
    }

    private void draw_vision_circle() //method for creating the visualization of enemy's vision field
    {
        float x;
        float z;

        float angle = 15f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * vision_field;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * vision_field;

            line.SetPosition(i, new Vector3(z, x, 0));

            angle += (360f / segments);
        }

    }

    private void searching(Vector2 dir) //method of Enemy searching for another object
    {
        Vector2 playerInput = dir;
        playerInput = Vector2.ClampMagnitude(playerInput, 1f); // We are clamping the retrieved input values to unit lenght

        Vector3 desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed; // We specify our desired velocity by multiplying maximum speed by user input
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        Vector3 displacement = velocity * Time.deltaTime; // We are calculating by how much our object is gonna move based on time, so that it doesnt move too fast or too slow
        Vector3 newPosition = transform.localPosition + displacement;
        newPosition.y = 0.26f;

        if (newPosition.x < allowedArea.xMin)
        {
            random_dir = Vector2.Reflect(random_dir, Vector2.right);
            newPosition.x = allowedArea.xMin;
            velocity.x = -velocity.x * bounciness;
        }
        else if (newPosition.x > allowedArea.xMax)
        {
            random_dir = Vector2.Reflect(random_dir, Vector2.left);
            newPosition.x = allowedArea.xMax;
            velocity.x = -velocity.x * bounciness;
        }

        if (newPosition.z < allowedArea.yMin)
        {
            random_dir = Vector2.Reflect(random_dir, Vector2.down);
            newPosition.z = allowedArea.yMin;
            velocity.z = -velocity.z * bounciness;
        }
        else if (newPosition.z > allowedArea.yMax)
        {
            random_dir = Vector2.Reflect(random_dir, Vector2.up);
            newPosition.z = allowedArea.yMax;
            velocity.z = -velocity.z * bounciness;
        }

        transform.localPosition = newPosition;

        update_rotation(playerInput);
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

    private void following(float timediff) //method making the Enemy object follow the object that entered the vision field
    {
        Vector2 dir = new Vector2(ObjectsInRange[0].transform.position.x - transform.position.x, ObjectsInRange[0].transform.position.z - transform.position.z);
        dir.Normalize();
        update_rotation(dir);
        transform.position = (walking_speed_enemy - timediff) * transform.position +  (1f - (walking_speed_enemy - timediff)) * (ObjectsInRange[0].transform.position);
    }

    private static float calculateAngle(Vector2 first, Vector2 second)
    {
        float angle_cos = Mathf.Acos((Vector2.Dot(first, second)) / (first.magnitude * second.magnitude));
        float result = (angle_cos * 180) / Mathf.PI;
        return result;
    }

    public void set_vision(float a)
    {
        vision_field = a;
    }

    public float get_vision()
    {
        return vision_field;
    }
}

