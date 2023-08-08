using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script for triggering dialogue */

public class DialogueTrigger : MonoBehaviour
{
    /*
     Dialogue triggering added to "HoveringPanel" script. This is Unused script.
     */

    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
