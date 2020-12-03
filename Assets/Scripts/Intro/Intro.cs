using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine("showParchment");
        FindObjectOfType<AudioManager>().Play("Rain");
        StartCoroutine("StartIntro1");
    }
    
    IEnumerator StartIntro1()
    {
        yield return new WaitForSeconds(14f);
        GetComponent<Animator>().Play("showPanelText");

    }
    IEnumerator closeIntro()
    {
        yield return new WaitForSeconds(1.95f);
        SceneManager.LoadScene("Main");
        //GetComponent<Animator>().Play("closeIntro");
    }
    /*
    IEnumerator showParchment()
    {
        Debug.Log("Holaa");
        yield return new WaitForSeconds(2f);
        //GetComponent<Animator>().Play("showParchment");
    }
    */
    // Update is called once per frame
    void Update()
    {
        
    }
}
