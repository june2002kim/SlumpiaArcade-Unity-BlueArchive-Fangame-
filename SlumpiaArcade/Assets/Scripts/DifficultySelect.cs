using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Script for selecting game's difficulty using ToggleGroup in UI */

public class DifficultySelect : MonoBehaviour
{
    ToggleGroup toggleGroup;

    // Start is called before the first frame update
    void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();

        /*
        PlayerPrefs.SetInt("Difficulty", 0);
        */
    }

    /*
     Change obstacle's spawning count or delay
     */

    public void Normal(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("Select : Normal");
            PlayerPrefs.SetInt("Difficulty", 0);

            PlayerPrefs.SetInt("KuroShootSet", 3);
            PlayerPrefs.SetInt("ShiroShootCount", 2);
            PlayerPrefs.SetFloat("KuroSpawnDelaySet", 5f);
            PlayerPrefs.SetFloat("ShiroSpawnDelaySet", 40f);
            PlayerPrefs.SetFloat("ShiroLaunchDelaySet", 7f);
        }
        else
        {
            Debug.Log("Discard : Normal");
            PlayerPrefs.SetInt("Difficulty", 1);

            PlayerPrefs.SetInt("KuroShootSet", 1);
            PlayerPrefs.SetInt("ShiroShootCount", 2);
            PlayerPrefs.SetFloat("KuroSpawnDelaySet", 1f);
            PlayerPrefs.SetFloat("ShiroSpawnDelaySet", 20f);
            PlayerPrefs.SetFloat("ShiroLaunchDelaySet", 7f);
        }
    }

    public void Hard(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("Select : Hard");
            PlayerPrefs.SetInt("Difficulty", 1);

            PlayerPrefs.SetInt("KuroShootSet", 1);
            PlayerPrefs.SetInt("ShiroShootCount", 2);
            PlayerPrefs.SetFloat("KuroSpawnDelaySet", 1f);
            PlayerPrefs.SetFloat("ShiroSpawnDelaySet", 20f);
            PlayerPrefs.SetFloat("ShiroLaunchDelaySet", 7f);
        }
        else
        {
            Debug.Log("Discard : Hard");
            PlayerPrefs.SetInt("Difficulty", 0);

            PlayerPrefs.SetInt("KuroShootSet", 3);
            PlayerPrefs.SetInt("ShiroShootCount", 2);
            PlayerPrefs.SetFloat("KuroSpawnDelaySet", 5f);
            PlayerPrefs.SetFloat("ShiroSpawnDelaySet", 40f);
            PlayerPrefs.SetFloat("ShiroLaunchDelaySet", 7f);
        }
    }
}
