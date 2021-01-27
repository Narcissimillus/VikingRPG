using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    public static bool GameOver;
    public static bool isExitting;
    public GameObject pauseMenu;

    private void Awake()
    {
        GameIsPaused = false;
        GameOver = false;
        isExitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameOver == false)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (GameIsPaused && Time.timeScale != 0)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0, 0.01f);
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        GameIsPaused = true;
    }

    public void LoadMenu()
    {

    }

    public void ExitGame()
    {
        isExitting = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void RestartGame()
    {
        isExitting = true;
        SceneManager.LoadScene("VikingRPG");
    }
}
