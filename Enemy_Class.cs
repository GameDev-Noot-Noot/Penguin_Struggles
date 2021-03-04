using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Class : MonoBehaviour
{
    private List<GameObject> ObjectsInRange = new List<GameObject>();
    [SerializeField, Range(0, 100)]
    SphereCollider enemyCollider;
    string state = "search"; 
    [SerializeField]
    float vision_field;
    [SerializeField]
    float walking_speed_enemy = 0.97f;
    public int segments = 50;
    public float xradius = 5;
    public float yradius = 5;
    LineRenderer line;


    void Start()
    {
        enemyCollider = GetComponent<SphereCollider>();
        line = gameObject.GetComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        Color c1 = new Color(1f, 1f, 0f, 1); 
        line.SetColors(c1, c1); //setting the vision field circle to yellow  

        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
    }
    void Update()
    {
        visionCircle();
        if (ObjectsInRange.Count != 0) { state = "follow"; } //making sure the state of Enemy object is changed if there is an object in the vision field
        else { state = "search"; }

        if (state == "search") { searching(); } //calling appropriate methods when Enemy's state is changed
        else { following();  }
    }


    public void OnTriggerEnter(Collider col) //method called when there is a Player object in the spehre collider radius of the Enemy object
    {
        if (col.gameObject.tag == "Player") //if there is, the vision field changes color to red
        {
            ObjectsInRange.Add(col.gameObject);
            Color c2 = new Color(1f, 0f, 0f, 1);
            line.SetColors(c2, c2);
        }
    }

    public void OnTriggerExit(Collider col) //method called when there is no object in the spehre collider radius of the Enemy object

    {
        if (col.gameObject.tag == "Player")
        {
            ObjectsInRange.Remove(col.gameObject); // changes the vision field circle to color yellow again
            Color c3 = new Color(1f, 1f, 0f, 1);
            line.SetColors(c3, c3);
        }
    }

    public void visionCircle() //method creating the visualization of enemy's vision field
    {
        vision_field = enemyCollider.radius;
        float x;
        float z;

        float angle = 15f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * vision_field;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * vision_field;

            line.SetPosition(i, new Vector3(x, 0, z));

            angle += (360f / segments);
        }

    }

    public void searching() //method of Enemy searching for another object
    {
       //add randomized locations the Enemy will travel to
    }

    public void following() //method making the Enemy object follow the object that entered the vision field
    {
        transform.position = walking_speed_enemy * transform.position +  (1f - walking_speed_enemy) * (ObjectsInRange[0].transform.position + new Vector3(1.2f, 0f, 1.2f));
    }

    /*
    static public Vector3 get_pos()
    {
        return transform.localPosition;
    }*/
}

