using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog_manager : MonoBehaviour
{
    public Dialogue dialogue;
    Queue<string> sentences;
    public TextMeshProUGUI displayText;
    public GameObject dialoguePanel;
    string activeSentence;
    public float typingSpeed;

    void Start()
    {
        sentences = new Queue<string>();
    }

    void startDialogue()
    {
        sentences.Clear();

        foreach(string sentence in dialogue.sentenceList)
        {
            sentences.Enqueue(sentence);
        }
        displayNextSentence();
    }

    void displayNextSentence()
    {
        if(sentences.Count <= 0)
        {
            displayText.text = activeSentence;
            return;
        }

        activeSentence = sentences.Dequeue();
        Debug.Log(activeSentence);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
