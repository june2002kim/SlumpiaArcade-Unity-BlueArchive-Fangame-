using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AbilitySelect : MonoBehaviour
{
    ToggleGroup toggleGroup;

    private void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();

        PlayerPrefs.SetInt("healthPointSet", 3);
        PlayerPrefs.SetInt("isHealthRegen", 0);

        PlayerPrefs.SetFloat("dashCooldownSet", 1f);
        PlayerPrefs.SetInt("isImmortalDash", 0);
        PlayerPrefs.SetFloat("dashDurationSet", 0.1f);
        PlayerPrefs.SetFloat("dashSpeedSet", 25f);

        PlayerPrefs.SetFloat("shieldCooldownSet", 10f);

        PlayerPrefs.SetFloat("moveSpeedSet", 8f);

        PlayerPrefs.SetFloat("ImmortalTimeSet", 1f);
    }

    

    public void healthUp(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("Select : Health UP");
            PlayerPrefs.SetInt("healthPointSet", 4);
        }
        else
        {
            Debug.Log("Discard : Health UP");
            PlayerPrefs.SetInt("healthPointSet", 3);
        }
    }

    public void moreDash(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("Select : more Dash");
            PlayerPrefs.SetFloat("dashCooldownSet", 0.5f);
        }
        else
        {
            Debug.Log("Discard : more Dash");
            PlayerPrefs.SetFloat("dashCooldownSet", 1f);
        } 
    }

    public void moreShield(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("Select : more Shield");
            PlayerPrefs.SetFloat("shieldCooldownSet", 5f);
        }
        else
        {
            Debug.Log("Discard : more Shield");
            PlayerPrefs.SetFloat("shieldCooldownSet", 10f);
        }
    }

    public void healthRegen(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("Select : health Regen");
            PlayerPrefs.SetInt("isHealthRegen", 1);
        }
        else
        {
            Debug.Log("Discard : health Regen");
            PlayerPrefs.SetInt("isHealthRegen", 0);
        }
    }

    public void immortalDash(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("Select : immortal Dash");
            PlayerPrefs.SetInt("isImmortalDash", 1);
            PlayerPrefs.SetFloat("dashDurationSet", 0.08f);
            //PlayerPrefs.SetFloat("dashSpeedSet", 20f);
        }
        else
        {
            Debug.Log("Discard : immortal Dash");
            PlayerPrefs.SetInt("isImmortalDash", 0);
            PlayerPrefs.SetFloat("dashDurationSet", 0.12f);
            //PlayerPrefs.SetFloat("dashSpeedSet", 25f);
        }
    }

    public void speedUp(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("Select : speed Up");
            PlayerPrefs.SetFloat("moveSpeedSet", 11f);
        }
        else
        {
            Debug.Log("Discard : speed Up");
            PlayerPrefs.SetFloat("moveSpeedSet", 8f);
        }
    }
    public void farDash(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("Select : far Dash");
            PlayerPrefs.SetFloat("dashDurationSet", 0.2f);
        }
        else
        {
            Debug.Log("Discard : far Dash");
            PlayerPrefs.SetFloat("dashDurationSet", 0.12f);
        }
    }

    public void lessHit(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("Select : less Hit");
            PlayerPrefs.SetFloat("ImmortalTimeSet", 2f);
        }
        else
        {
            Debug.Log("Discard : less Hit");
            PlayerPrefs.SetFloat("ImmortalTimeSet", 1f);
        }
    }
}
