using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/* Script for main menu in scene "Menu" */

public class MainMenu : MonoBehaviour
{
    public GameObject shirokuroPanel;
    public GameObject gozPanel;

    public TMP_Text shirokuroRecord;
    public TMP_Text gozRecord;

    private void Start()
    {
        // set timescale back to '1' because returning from paused menu makes timescale '0' 
        Time.timeScale = 1;

        // get each games record from 'PlayerPrefs'
        shirokuroRecord.text = "" + PlayerPrefs.GetFloat("ShiroKuroRecord");
        gozRecord.text = "" + PlayerPrefs.GetFloat("GozRecord");
    }

    public void panelShiroKuro()
    {
        /*
         When ShiroKuro panel has been selected, set game's standard values 
        to make it works when not selecting any panel menu and starts game right away
         */

        shirokuroPanel.SetActive(true);

        // values for ability
        PlayerPrefs.SetInt("healthPointSet", 3);
        PlayerPrefs.SetInt("isHealthRegen", 0);

        PlayerPrefs.SetFloat("dashCooldownSet", 1f);
        PlayerPrefs.SetInt("isImmortalDash", 0);
        PlayerPrefs.SetFloat("dashDurationSet", 0.1f);
        PlayerPrefs.SetFloat("dashSpeedSet", 25f);

        PlayerPrefs.SetFloat("shieldCooldownSet", 10f);

        PlayerPrefs.SetFloat("moveSpeedSet", 8f);

        PlayerPrefs.SetFloat("ImmortalTimeSet", 1f);

        // which difficulty it is
        PlayerPrefs.SetInt("Difficulty", 0);

        // values for difficulty
        PlayerPrefs.SetInt("KuroShootSet", 3);
        PlayerPrefs.SetInt("ShiroShootCount", 2);
        PlayerPrefs.SetFloat("KuroSpawnDelaySet", 5f);
        PlayerPrefs.SetFloat("ShiroSpawnDelaySet", 40f);
        PlayerPrefs.SetFloat("ShiroLaunchDelaySet", 7f);
    }

    public void panelGoz()
    {
        gozPanel.SetActive(true);
    }

    public void startShiroKuro()
    {
        /*
         Start ShiroKuro game loading scene "ShiroKuro"
         */

        //Time.timeScale = 1;
        SceneManager.LoadScene("ShiroKuro");
    }

    public void startGoz()
    {
        //Time.timeScale = 1;
        SceneManager.LoadScene("Goz");
    }
}
