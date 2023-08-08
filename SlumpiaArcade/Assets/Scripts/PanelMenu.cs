using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Script for panel menu in "Main" scene */

public class PanelMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            returnMain();
        }
    }

    public void returnMain()
    {
        /* return to "Main" scene by setting panel's activation to false */

        gameObject.SetActive(false);
    }
}
