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
}
