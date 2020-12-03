using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class panelText : MonoBehaviour
{
    public Dialogue dialogue;
    Queue<string> sentences;
    public Text displayText;
    string activeSentence;
    public float typingSpeed;
    public GameObject Intro;
    public GameObject panel;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        StartCoroutine("startAnimation");
    }
    IEnumerator startAnimation()
    {
        yield return new WaitForSeconds(15f);
        startDialogue();
        displayNextSentence();
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
            gameObject.SetActive(false);
            return;
        }
        activeSentence = sentences.Dequeue();
        displayText.text = activeSentence;
        StopAllCoroutines();
        StartCoroutine(typeTheSentence(activeSentence));
    }

    IEnumerator typeTheSentence(string sentence)
    {
        int conteo = 0;
        displayText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            FindObjectOfType<AudioManager>().Play("Typing");
            displayText.text += letter;
            if (letter == '.')
            {
                conteo++;
            }
            if (letter == '%')
            {
                yield return new WaitForSeconds(2f);
            }
            else if (letter == ' ')
            {
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        yield return new WaitForSeconds(1);
        if(count == 0)
        {
            count++;
            //displayNextSentence();
            displayText.text = "";
            panel.SetActive(false);
            Intro.GetComponent<Animator>().Play("shake");
            displayText.text = dialogue.sentenceList[1];
            Intro.GetComponent<Intro>().StartCoroutine("closeIntro");
        }
        //Intro.GetComponent<Animator>().Play("showText1");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
