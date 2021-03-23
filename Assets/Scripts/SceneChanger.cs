using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GoToWinScene()
    {
        SceneManager.LoadScene("You_won");
    }

    public void GoToLossScene()
    {
        SceneManager.LoadScene("You_lost");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
