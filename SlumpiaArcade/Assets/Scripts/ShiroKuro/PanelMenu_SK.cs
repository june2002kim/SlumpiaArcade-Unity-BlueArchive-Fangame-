using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
        GameManager_SK.instance.isPaneled = false;
        GameManager_SK.instance.pauseGame();
        gameObject.SetActive(false);
    }

    public void SetVolumeBGM(float volumeBGM)
    {
        BGMaudioMixer.SetFloat("volumeBGM", volumeBGM);
    }

    public void SetVolumeSFX(float volumeSFX)
    {
        SFXaudioMixer.SetFloat("volumeSFX", volumeSFX);
    }
}
