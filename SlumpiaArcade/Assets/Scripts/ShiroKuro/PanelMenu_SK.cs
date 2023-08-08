using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/* Script for panel menu in "ShiroKuro" scene */

public class PanelMenu_SK : MonoBehaviour
{
    public AudioMixer BGMaudioMixer;
    public AudioMixer SFXaudioMixer;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            returnPause();
        }
    }

    public void returnPause()
    {
        /*
         When panel menu has activated when game is paused, set 'isPaneled' to false and call 'pauseGame()' and set panel's activation to false
         */

        GameManager_SK.instance.isPaneled = false;
        GameManager_SK.instance.pauseGame();
        gameObject.SetActive(false);
    }

    public void SetVolumeBGM(float volumeBGM)
    {
        /*
         Set BGM's volume with slider in UI and 'BGMaudioMixer'
         */

        BGMaudioMixer.SetFloat("volumeBGM", volumeBGM);
    }

    public void SetVolumeSFX(float volumeSFX)
    {
        /*
         Set SFX's volume with slider in UI and 'SFXaudioMixer'
         */

        SFXaudioMixer.SetFloat("volumeSFX", volumeSFX);
    }
}
