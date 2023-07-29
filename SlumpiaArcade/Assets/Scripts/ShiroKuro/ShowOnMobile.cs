using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
