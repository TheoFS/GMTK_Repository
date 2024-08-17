using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    ChangeSceneBehaviour changeSceneBehaviourScript;

    private void Start()
    {
        changeSceneBehaviourScript = GetComponent<ChangeSceneBehaviour>();
    }
    public void PlayButton()
    {
        changeSceneBehaviourScript.ChangeScene("TemplateScene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }


}
