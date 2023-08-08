using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/* Script for showing description panel when hovering ability icon using UnityEngine's EventSystems */

public class HoveringPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverPanel;
    public Dialogue dialogue;

    public void OnPointerEnter(PointerEventData eventData)
    {
        /*
         When mouse pointer enters ability icon, it starts dialogue
         */

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        //hoverPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        /*
         When mouse pointer exits ability icon, set description panel's active to 'false'
         */

        hoverPanel.SetActive(false);
    }
}
