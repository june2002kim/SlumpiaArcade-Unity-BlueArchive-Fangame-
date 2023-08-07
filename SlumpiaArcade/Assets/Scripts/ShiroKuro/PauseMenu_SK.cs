using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu_SK : MonoBehaviour
{
    public GameObject soundPanel;
    public GameObject abilityPanel;

    public void loadPanel()
    {
        GameManager_SK.instance.isPaneled = true;
    }

    public void returnMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void restartGame()
    {
        SceneManager.LoadScene("ShiroKuro");
    }

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
