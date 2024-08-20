using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject creditsMenu;
    [SerializeField] GameObject controlsMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject licensesMenu;
    ChangeSceneBehaviour changeSceneBehaviourScript;

    private bool menuMusicPlaying;

    private void Start()
    {
        changeSceneBehaviourScript = GetComponent<ChangeSceneBehaviour>();
        optionsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        StartCoroutine(MenuMusicPlaying());
    }

    private void Update()
    {
        if (menuMusicPlaying == false)
        {
            StartCoroutine(MenuMusicPlaying());
        }
    }

    IEnumerator MenuMusicPlaying()
    {
        MusicManager.PlaySFX("Menu Music");
        menuMusicPlaying = true;
        yield return new WaitForSeconds(13.6f);
        menuMusicPlaying = false;
    }

    public void PlayButton()
    {
        changeSceneBehaviourScript.ChangeScene("TemplateScene");
    }

    public void OptionsButton()
    {
       optionsMenu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ReturnButton()
    {
        optionsMenu.SetActive(false);
    }

    public void CreditsButton()
    {
        creditsMenu.SetActive(true);
        
    }

    public void CreditsReturn()
    {
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void ControlsButton()
    {
        controlsMenu.SetActive(true);
    }

    public void ControlsReturn()
    {
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
    }

    public void ReturnSettings()
    {
        settingsMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void Page1Button()
    {
        licensesMenu.SetActive(false);
    }

    public void Page2Button()
    {
        licensesMenu.SetActive(true);
    }

    public void ReturnLicenses()
    {
        licensesMenu.SetActive(false);
        creditsMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }
}
