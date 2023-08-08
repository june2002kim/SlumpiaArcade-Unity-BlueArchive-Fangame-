using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* Script for managing dialogue */

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    public GameObject dialogueBox;                      // ability description panel where dialogue is going to be shown
    public TMP_Text nameText;                           // ability description panel's name component
    public TMP_Text dialogueText;                       // ability description panel's content component
    public Image portraitImage;                         // ability description panel's image component

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        /*
         Set ability description's components to parameter 'dialogue' and starts dialogue
         */

        // show ability description panel when dialogue starts
        dialogueBox.SetActive(true);

        portraitImage.sprite = dialogue.portrait;
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        if (dialogue.isTyping)
        {
            TypeNextSentence();
        }
        else
        {
            DisplayNextSentence();
        }
    }

    public void TypeNextSentence()
    {
        /*
         If there's left sentence, add sentence to ability description panel's content each letter in order by corountine
         */

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayNextSentence()
    {
        /*
         If there's left sentence, add sentence to ability description panel's content at once
         */

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    IEnumerator TypeSentence (string sentence)
    {
        /*
         add sentence's each letter to ability description panel in order
         */

        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        //dialogueBox.SetActive(false);
    }
}
