using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    ChangeSceneBehaviour changeSceneBehaviourScript;
    HUDManager hudManagerScript;

    private void Start()
    {
        changeSceneBehaviourScript = GetComponent<ChangeSceneBehaviour>();
        hudManagerScript = GameObject.FindGameObjectWithTag("Hud").GetComponent<HUDManager>();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        hudManagerScript.gameIsPaused = false;
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        changeSceneBehaviourScript.ChangeScene("MenuScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
