using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainuMenu : MonoBehaviour
{
    public void StartGame()
    {
        PauseMenu.GameIsPaused = false;
        SceneManager.LoadScene("VikingRPG");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }
}
