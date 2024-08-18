using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject creditsMenu;
    [SerializeField] GameObject controlsMenu;
    ChangeSceneBehaviour changeSceneBehaviourScript;

    private void Start()
    {
        changeSceneBehaviourScript = GetComponent<ChangeSceneBehaviour>();
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

}
