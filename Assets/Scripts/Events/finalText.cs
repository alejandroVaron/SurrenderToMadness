using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class finalText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("textFinal");
    }
    private IEnumerator textFinal()
    {
        yield return new WaitForSeconds(1.1f);
        string sentence = "Continuara...";
        foreach (char letter in sentence.ToCharArray())
        {
            FindObjectOfType<AudioManager>().Play("Typing");
            GetComponent<Text>().text += letter;
            yield return new WaitForSeconds(0.095f);
          
        }
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu");     
    }

}
