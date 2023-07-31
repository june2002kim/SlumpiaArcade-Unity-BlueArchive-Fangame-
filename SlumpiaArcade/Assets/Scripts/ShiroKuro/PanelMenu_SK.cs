using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMenu_SK : MonoBehaviour
{
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
}
