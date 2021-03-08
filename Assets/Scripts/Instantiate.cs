using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    [SerializeField]
    static int fish_per_square = 3;

    public GameObject prefab;
    public GameObject fish_count;

    //private List<GameObject> existing_fishes = new List<GameObject>();

    void Start()
    {
        populate();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount < 1)
        {
            print("poop");
            populate();
        }
    }

    private void populate()
    {
        for (int i = 0; i < fish_per_square; i++)
        {
            Vector3 random_pos = new Vector3(Random.Range(-5f, 5f), 0.1f, Random.Range(-5f, 5f));
            GameObject e = Instantiate(prefab, random_pos, Quaternion.Euler(90, 0, 0));
            e.transform.SetParent(gameObject.transform);
            e.GetComponent<Fish>().assign_text(fish_count);
            //existing_fishes.Add(e);
        }
    }
}
