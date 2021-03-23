using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    [SerializeField]
    static int fish_per_square = 3;

    public GameObject prefab;
    public GameObject enemy;
    public GameObject player;
    public GameObject gui;
    public GameObject switcher;
    public GameObject camera;
    public GameObject igloo;
    public GameObject pack;

    private GameObject player_instance;
    private GameObject pack_instance;

    void Start()
    {
        populate_fish();
        populate_enemy();

        player_instance = Instantiate(player, transform.position, Quaternion.Euler(90, 0, 0));
        player_instance.GetComponent<Player_Movement>().set_health_bar(gui.GetComponent<gui_methods>().get_health_bar());
        player_instance.GetComponent<Player_Movement>().set_scene_switcher(switcher);
        player_instance.GetComponent<Player_Movement>().set_igloo_and_gui(igloo, gui);

        camera.GetComponent<Camera_Movement>().set_followed(player_instance);

        pack_instance = Instantiate(pack, transform.position, Quaternion.Euler(90, 0, 0));
        pack_instance.GetComponent<Pack>().set_followed(player_instance);
        player_instance.GetComponent<Player_Movement>().set_pack(pack_instance);
    }

    void Update()
    {
        if (gameObject.transform.childCount < 1)
        {
            populate_fish();
            populate_enemy();
            pack_instance.GetComponent<Pack>().instantiate_child(random_pos());
            //GameObject p = Instantiate(child, random_pos(), Quaternion.Euler(90, 0, 0));
        }
    }

    private void populate_fish()
    {
        for (int i = 0; i < fish_per_square; i++)
        {
            GameObject e = Instantiate(prefab, random_pos(), Quaternion.Euler(0, 0, 0));
            e.transform.SetParent(gameObject.transform);
            e.GetComponent<Fish>().assign_text(gui.GetComponent<gui_methods>().get_fish_counter());
        }
    }

    private void populate_enemy()
    {
        
        Instantiate(enemy, random_pos(), Quaternion.Euler(90, 0, 0));
    }

    private Vector3 random_pos()
    {
        Vector3 random_position = new Vector3(Random.Range(-5f, 5f), 0.1f, Random.Range(-5f, 5f));
        return random_position;
    }
}
