using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public bool isTyping;

    public Sprite portrait;

    public string name;

    [TextArea(3, 20)]
    public string[] sentences;
}
