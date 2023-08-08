using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Script for dialogue */

[System.Serializable]
public class Dialogue
{
    public bool isTyping;           // decide typing effect in "DialogueManager"

    public Sprite portrait;         // dialogue's image

    public string name;             // dialogue's name

    [TextArea(3, 20)]
    public string[] sentences;      // dialogue's content
}
