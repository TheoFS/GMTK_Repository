using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    PlayerManager playerManagerScript;

    private bool damageSoundPlaying;

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
    }

    IEnumerator PlayerDamage()
    {
        SFXManager.PlaySFX("DamageBark");
        damageSoundPlaying = true;
        yield return null;  
    }
}
