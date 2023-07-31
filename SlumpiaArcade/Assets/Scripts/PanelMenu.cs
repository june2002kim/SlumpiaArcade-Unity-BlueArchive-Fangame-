using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        gameObject.SetActive(false);
    }
}
