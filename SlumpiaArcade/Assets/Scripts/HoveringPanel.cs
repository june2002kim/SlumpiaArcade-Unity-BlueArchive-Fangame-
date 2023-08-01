using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoveringPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverPanel;
    public Dialogue dialogue;

    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        //hoverPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverPanel.SetActive(false);
    }
}
