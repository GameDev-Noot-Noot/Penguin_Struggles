using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_switcher : MonoBehaviour
{
    public void GotoMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void GotoMenuScene()
    {
        SceneManager.LoadScene("Main_menu");
    }

    public void quit()
    {
        Application.Quit();
    }

    public void instructions()
    {
        SceneManager.LoadScene("instructions");
    }

    public void win_screen()
    {
        SceneManager.LoadScene("win_menu");
    }

    public void lose_screen()
    {
        SceneManager.LoadScene("lose_menu");
    }

    public void intro_screen()
    {
        SceneManager.LoadScene("intro_screen");
    }
}
