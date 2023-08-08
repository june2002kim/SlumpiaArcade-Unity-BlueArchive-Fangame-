using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Script for pause menu in scene "ShiroKuro" */

public class PauseMenu_SK : MonoBehaviour
{
    public GameObject soundPanel;
    public GameObject abilityPanel;

    public void loadPanel()
    {
        /*
         When panel is loaded when game is paused, set 'isPaneled' to 'true'
         */

        GameManager_SK.instance.isPaneled = true;
    }

    public void returnMain()
    {
        /*
         Return to scene "Main"
         */

        SceneManager.LoadScene("Main");
    }

    public void restartGame()
    {
        /*
         Restart game by reloading scene "ShiroKuro"
         */

        SceneManager.LoadScene("ShiroKuro");
    }

    public void quitGame()
    {
        /*
         Quit game
         */

        //Debug.Log("Quit");
        Application.Quit();
    }
}
