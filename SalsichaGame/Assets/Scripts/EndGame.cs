using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    PlayerManager playerManagerScript;
    SFXManager sfxManagerScript;

    void Start()
    {
        playerManagerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        sfxManagerScript = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManagerScript.victorious == true)
        {
            sfxManagerScript.endgame = true;
        }
    }
}
