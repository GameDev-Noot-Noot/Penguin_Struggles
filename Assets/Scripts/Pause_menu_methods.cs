using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pause_menu_methods : MonoBehaviour
{
    // Start is called before the first frame update
    public void destroy_itself()
    {
        Time.timeScale = 1f;
        Destroy(gameObject);
    }

    // Update is called once per frame
    public void main_menu()
    {
        SceneManager.LoadScene("Main_menu");
        Time.timeScale = 1f;
    }
}
