using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Asteroid

public class ButtonManagerScript : MonoBehaviour
{
    public GameObject pauseScreen;

    public void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public void quitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void backButton()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;

    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene("VolumeSettingMenu");
    }

    public void Controls()
    {
        SceneManager.LoadScene("HowToPlayMenu");
    }
}
