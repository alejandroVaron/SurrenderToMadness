using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class npcController : MonoBehaviour
{
    GameObject player;
    Animator anim;
    bool enter;

    public Dialogue dialogue;
    Queue<string> sentences;
    public string nameNpc;
    public TextMeshProUGUI displayText;
    public GameObject dialoguePanel;
    string activeSentence;
    public float typingSpeed;
    GameObject imageFaceSet;
    public GameObject question;
    bool questionValide;
    bool response;
    void Start()
    {
        sentences = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        if (nameNpc == "Ritha")
        {
            imageFaceSet = dialoguePanel.transform.GetChild(3).gameObject;
        }else if (nameNpc == "Xavier")
        {
            imageFaceSet = dialoguePanel.transform.GetChild(2).gameObject;
        }else if (nameNpc == "Lina")
        {
            imageFaceSet = dialoguePanel.transform.GetChild(1).gameObject;
        }

    }

    void startDialogue()
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentenceList)
        {
            sentences.Enqueue(sentence);
        }
    }

    void displayNextSentence()
    {
        if (sentences.Count <= 0)
        {
            displayText.text = activeSentence;
            dialoguePanel.SetActive(false);
            return;
        }
        activeSentence = sentences.Dequeue();
        if (activeSentence == "")
        {
            questionValide = true;
        }
        else
        {
            questionValide = false;
        }
        displayText.text = activeSentence;
        StopAllCoroutines();
        StartCoroutine(typeTheSentence(activeSentence));
    }

    IEnumerator typeTheSentence(string sentence)
    {
        if (!questionValide)
        {
            displayText.text = nameNpc + ": ";
            foreach (char letter in sentence.ToCharArray())
            {
                FindObjectOfType<AudioManager>().Play("Typing");
                displayText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }

    void Update()
    {
        if (enter)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialoguePanel.SetActive(true);
                imageFaceSet.SetActive(true);
                Vector3 dir = (player.transform.position - transform.position).normalized;
            anim.SetBool("dir", true);
            anim.SetFloat("movx", dir.x);
            anim.SetFloat("movy", dir.y);
                if (!questionValide)
                {
                    displayNextSentence();
                }
                else
                {
                    //question.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            // anim.SetBool("dir", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enter = true;
            startDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enter = false;
            dialoguePanel.SetActive(false);
            imageFaceSet.SetActive(false);
        }
    }
}
