using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    [SerializeField]
    static int fish_per_square = 3;

    public GameObject prefab;
    public GameObject penguin_counter;
    public GameObject enemy;
    public GameObject player;
    public GameObject gui;
    public GameObject switcher;
    public GameObject camera;
    public GameObject igloo;
    public GameObject pack;

    private GameObject player_instance;
    private GameObject pack_instance;
    private float enemy_vision;

    private int populate_iterations = 1;

    void Start()
    {
        player_instance = Instantiate(player, transform.position, Quaternion.Euler(90, 0, 0));
        player_instance.GetComponent<Player_Movement>().set_health_bar(gui.GetComponent<gui_methods>().get_health_bar());
        player_instance.GetComponent<Player_Movement>().set_scene_switcher(switcher);
        player_instance.GetComponent<Player_Movement>().set_igloo_and_gui(igloo, gui);

        camera.GetComponent<Camera_Movement>().set_followed(player_instance);

        pack_instance = Instantiate(pack, transform.position, Quaternion.Euler(90, 0, 0));
        pack_instance.GetComponent<Pack>().set_followed(player_instance);
        pack_instance.GetComponent<Pack>().set_text(penguin_counter);

        player_instance.GetComponent<Player_Movement>().set_pack(pack_instance);

        populate_fish();
        populate_enemy(calculate_player_area(player_instance.GetComponent<Player_Movement>().get_player_pos()));

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemy_vision = enemies[0].GetComponent<Enemy>().get_vision();
    }

    void Update()
    {
        if (gameObject.transform.childCount < 1)
        {
            FindObjectOfType<AudioManager>().Play("newPenguin");
            populate_fish();
            if (populate_iterations % 2 == 0)
            {
                populate_enemy(calculate_player_area(player_instance.GetComponent<Player_Movement>().get_player_pos()));
            }

            pack_instance.GetComponent<Pack>().instantiate_child(random_pos());
            populate_iterations += 1;
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

    private void populate_enemy(Rect area)
    {
        while(true)
        {
            Vector3 r = random_pos();

            if (area.Contains(r) == false)
            {
                Instantiate(enemy, r, Quaternion.Euler(90, 0, 0));
                break;
            }
        }
        
    }

    private Vector3 random_pos()
    {
        Vector3 random_position = new Vector3(Random.Range(-5f, 5f), 0.26f, Random.Range(-5f, 5f));
        return random_position;
    }

    private Rect calculate_player_area(Vector2 pos)
    {
        Rect area = new Rect(pos.x - 2f, pos.y - 2f, 4f, 4f);
        return area;
    }

    private void update_enemy_vision(float ammount)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemy_vision += ammount;

        foreach (GameObject a in enemies)
        {
            a.GetComponent<Enemy>().set_vision(enemy_vision);
        }
    }
}
