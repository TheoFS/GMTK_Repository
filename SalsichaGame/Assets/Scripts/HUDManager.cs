using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Slider wienerBar;

    [SerializeField] GameObject pauseMenu;
    PlayerManager playerManagerScript;

    public bool gameIsPaused = false;

    public void Start()
    {
        playerManagerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        wienerBar.maxValue = playerManagerScript.wienerPoints;
    }

    public void Update()
    {
        wienerBar.value = playerManagerScript.wienerPoints;

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!gameIsPaused)
            {
                pauseMenu.SetActive(true);
                gameIsPaused = true;
                Time.timeScale = 0f;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
                gameIsPaused = false;
            }
        }
    }
}
