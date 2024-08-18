using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
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

}
