using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script for allows activating mobile controller in Mobile Platform include WebGL */

public class ShowOnMobile : MonoBehaviour
{
    public GameObject mobileController;

    private void Awake()
    {
        if (Application.isMobilePlatform)
        {
            mobileController.SetActive(true);
        }
    }
}
