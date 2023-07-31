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
        shirokuroRecord.text = "" + PlayerPrefs.GetFloat("ShiroKuroRecord");
        gozRecord.text = "" + PlayerPrefs.GetFloat("GozRecord");
    }

    public void panelShiroKuro()
    {
        shirokuroPanel.SetActive(true);
    }

    public void panelGoz()
    {
        gozPanel.SetActive(true);
    }

    public void startShiroKuro()
    {
        SceneManager.LoadScene("ShiroKuro");
    }

    public void startGoz()
    {
        SceneManager.LoadScene("Goz");
    }
}
