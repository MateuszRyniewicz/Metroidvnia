using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static PauseController instance;

    public string mainMenu;
    public GameObject pauseObject;
    public bool isPaused;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        } 
    }
    public void PauseUnpause()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            isPaused = true;
            pauseObject.SetActive(true);
            Time.timeScale = 0;
        }

    }


    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1;

    }
}
