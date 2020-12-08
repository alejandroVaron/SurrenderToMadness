using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelBrotherSword : MonoBehaviour
{
    public GameObject check;
    public GameObject details2;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        StartCoroutine("transition");
    }
    IEnumerator transition()
    {
        string sentence = "¡La espada de mi hermano!";
        foreach (char letter in sentence.ToCharArray())
        {
            FindObjectOfType<AudioManager>().Play("Typing");
            transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text += letter;
            if(letter == ' ')
            {
                yield return new WaitForSeconds(0.03f);
            }
            else
            {
                yield return new WaitForSeconds(0.06f);
            }
        }
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<Animator>().Play("NotificationMission");
        check.gameObject.SetActive(true);
        details2.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.7f);
        gameObject.SetActive(false);

    }
    IEnumerator typeTheSentence(string sentence)
    {
            foreach (char letter in sentence.ToCharArray())
            {
                FindObjectOfType<AudioManager>().Play("Typing");
                transform.GetChild(0).GetComponent<Text>().text += letter;
                yield return new WaitForSeconds(0.06f);
            }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
