using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject shirokuroPanel;
    public GameObject gozPanel;

    public TMP_Text shirokuroRecord;
    public TMP_Text gozRecord;

    private void Start()
    {
        Time.timeScale = 1;
        shirokuroRecord.text = "" + PlayerPrefs.GetFloat("ShiroKuroRecord");
        gozRecord.text = "" + PlayerPrefs.GetFloat("GozRecord");
    }

    public void panelShiroKuro()
    {
        shirokuroPanel.SetActive(true);

        PlayerPrefs.SetInt("healthPointSet", 3);
        PlayerPrefs.SetInt("isHealthRegen", 0);

        PlayerPrefs.SetFloat("dashCooldownSet", 1f);
        PlayerPrefs.SetInt("isImmortalDash", 0);
        PlayerPrefs.SetFloat("dashDurationSet", 0.1f);
        PlayerPrefs.SetFloat("dashSpeedSet", 25f);

        PlayerPrefs.SetFloat("shieldCooldownSet", 10f);

        PlayerPrefs.SetFloat("moveSpeedSet", 8f);

        PlayerPrefs.SetFloat("ImmortalTimeSet", 1f);


        PlayerPrefs.SetInt("Difficulty", 0);


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
        //Time.timeScale = 1;
        SceneManager.LoadScene("ShiroKuro");
    }

    public void startGoz()
    {
        //Time.timeScale = 1;
        SceneManager.LoadScene("Goz");
    }
}
