using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelBossText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("typeTheSentence");
    }

    IEnumerator typeTheSentence()
    {
        string sentence = "...";
            foreach (char letter in sentence.ToCharArray())
            {
                FindObjectOfType<AudioManager>().Play("Typing");
                transform.GetChild(0).GetComponent<Text>().text += letter;
                yield return new WaitForSeconds(0.5f);
            }
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
