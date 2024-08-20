using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    PlayerManager playerManagerScript;

    private bool damageSoundPlaying = false;
    private bool victoriousPlaying = false;

    private void Start()
    {
        playerManagerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (playerManagerScript.defeated & damageSoundPlaying == false)
        {
            StartCoroutine(PlayerDamage());
        }

        if (playerManagerScript.victorious & victoriousPlaying == false)
        {
            StartCoroutine(VictoriousSound());
        }
    }

    IEnumerator PlayerDamage()
    {
        SFXManager.PlaySFX("DamageBark");
        damageSoundPlaying = true;
        yield return null;  
    }

    IEnumerator VictoriousSound()
    {
        SFXManager.PlaySFX("VictoryBark");
        victoriousPlaying = true;
        yield return null;
    }
}
