using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string StartScene;

    public void Start()
    {
        AudioManager.instance.MainMenuMusic();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(StartScene);
       // PlayerController.instance.gameObject.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(StartScene);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
