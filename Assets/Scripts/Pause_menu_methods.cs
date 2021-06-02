using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Pause_menu_methods : MonoBehaviour
{
    string[] hints = { "Press 'SPACE' to build a base, where your baby penguins are invulnerable.",
                        "Try to avoid loosing baby penguins as enemy numbers wont decrease...",
                        "Collect 3 fishes to get a new baby penguin.",
                        "If your penguins are following you and you get attacked, your baby penguins will be targeted instead."};
    void Start()
    {
        update_hint();
    }

    public void destroy_itself()
    {
        Time.timeScale = 1f;
        Destroy(gameObject);
    }

    public void main_menu()
    {
        SceneManager.LoadScene("Main_menu");
        Time.timeScale = 1f;
    }

    public void update_hint()
    {
        Text p = transform.GetChild(transform.childCount - 1).GetComponent<Text>();
        p.text = "Hint: " + hints[Random.Range(0, 4)];
    }
}
