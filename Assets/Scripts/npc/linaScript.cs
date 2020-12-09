using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class linaScript : MonoBehaviour
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
    bool questionValide = false;
    public int response =0 ;
    public Text QuestionText;
    int count = 0;
    public GameObject mission;
    bool acceptMission = false;
    int statusMission = 0;
    void Start()
    {
        sentences = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        if (nameNpc == "Ritha")
        {
            imageFaceSet = dialoguePanel.transform.GetChild(3).gameObject;
        }
        else if (nameNpc == "Xavier")
        {
            imageFaceSet = dialoguePanel.transform.GetChild(2).gameObject;
        }
        else if (nameNpc == "Lina")
        {
            imageFaceSet = dialoguePanel.transform.GetChild(1).gameObject;
        }

    }

    void startDialogue()
    {
        if(statusMission == 1)
        {
            acceptMission = true;
        }else if(statusMission == 2)
        {
            acceptMission = false;
        }
        sentences.Clear();
        count = 0;
        questionValide = false;
        question.gameObject.SetActive(false);
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
                if(letter == ',')
                {
                    yield return new WaitForSeconds(0.4f);
                }else
                {
                    yield return new WaitForSeconds(typingSpeed);
                }
            }
        }
        else
        {
            QuestionText.text = nameNpc + ": ";
            foreach (char letter in sentence.ToCharArray())
            {
                FindObjectOfType<AudioManager>().Play("Typing");
                QuestionText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            questionValide = false;
        }
    }

    void Update()
    {
        if (enter)
        {
            if (Input.GetKeyDown(KeyCode.E) || response == 1 || response == 2)
            {
                StopAllCoroutines();
                Vector3 dir = (player.transform.position - transform.position).normalized;
                anim.SetBool("dir", true);
                anim.SetFloat("movx", dir.x);
                anim.SetFloat("movy", dir.y);
                if (!acceptMission)
                {
                    if (count == 0)
                    {
                        count++;
                        dialoguePanel.SetActive(true);
                        displayText.gameObject.SetActive(true);
                        imageFaceSet.SetActive(true);
                        StartCoroutine(typeTheSentence("Estuve buscando las perlas para hacer los ensambles..."));
                    }
                    else if (count == 1)
                    {
                        count++;
                        StopAllCoroutines();
                        StartCoroutine(typeTheSentence("pero no las encuentro"));
                    }
                    else if (count == 2)
                    {
                        count++;
                        StopAllCoroutines();
                        StartCoroutine(typeTheSentence("Me dijeron que se encontraban en el fondo de las montañas"));
                    }
                    else if (count == 3)
                    {
                        count++;
                        StopAllCoroutines();
                        StartCoroutine(typeTheSentence("Si alguien me trajera un par, podría llevarse parte de la recompensa..."));
                    }
                    else if (count == 4)
                    {
                        count++;
                        StopAllCoroutines();
                        displayText.gameObject.SetActive(false);
                        questionValide = true;
                        question.gameObject.SetActive(true);
                        player.GetComponent<player>().inDialogue = true;
                        imageFaceSet.SetActive(false);
                        StartCoroutine(typeTheSentence("¿Estás dispuesto?"));
                    }
                    else if (count == 5)
                    {
                        StopAllCoroutines();
                        questionValide = false;
                        player.GetComponent<player>().inDialogue = false;
                        question.gameObject.SetActive(false);
                        if (response == 1)
                        {
                            count++;
                            GameObject.FindGameObjectWithTag("Canvas").GetComponent<Animator>().Play("NotificationMission");
                            statusMission = 1;
                            displayText.gameObject.SetActive(true);
                            imageFaceSet.SetActive(true);
                            StartCoroutine(typeTheSentence("¡Oh! que alegria, avisame cuando los tengas"));         
                            mission.gameObject.SetActive(true);
                        }
                        else if (response == 2)
                        {
                            count++;
                            statusMission = 2;
                            displayText.gameObject.SetActive(true);
                            imageFaceSet.SetActive(true);
                            StartCoroutine(typeTheSentence("No hay problema, cuando cambies de opinión puedes buscarme"));
                        }
                        response = 0;
                    }
                    else if (count == 6)
                    {
                        dialoguePanel.SetActive(false);
                        imageFaceSet.SetActive(false);
                    }
                }else
                {
                    if (count == 0)
                    {
                        count++;
                        StopAllCoroutines();
                        dialoguePanel.SetActive(true);
                        displayText.gameObject.SetActive(true);
                        imageFaceSet.SetActive(true);
                        StartCoroutine(typeTheSentence("¿Hubo algún avance?"));
                    }else if (count == 1)
                    {
                        dialoguePanel.SetActive(false);
                        imageFaceSet.SetActive(false);
                    }
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
            Debug.Log("Entre");
            enter = true;
            startDialogue();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enter = false;
            StopAllCoroutines();
            dialoguePanel.SetActive(false);
            imageFaceSet.SetActive(false);
            count = 0;
        }
    }
}
