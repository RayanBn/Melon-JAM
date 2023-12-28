using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string LevelToLoad = "Game";
    public GameObject settingsMenu;

    public void startGame()
    {
        SceneManager.LoadScene(LevelToLoad);
    }

    public void settingsButton()
    {
        settingsMenu.SetActive(true);
    }

    public void closeSettingsButton()
    {
        settingsMenu.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
