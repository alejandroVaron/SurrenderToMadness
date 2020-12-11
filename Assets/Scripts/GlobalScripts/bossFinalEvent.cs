using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class bossFinalEvent : MonoBehaviour
{
    bool start = false;
    bool isFadeIn = false;
    float alpha = 0;
    float fadeTime = 1f;
    GameObject player;
    GameObject canvas;
    public GameObject target;
    public GameObject manager;
    public GameObject textPanel;
    public GameObject textCredits;
    public GameObject flash;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine("finalTransition");
        }
    }

    private IEnumerator soundWalk()
    {
        FindObjectOfType<AudioManager>().Play("walkEffect");
        yield return new WaitForSeconds(3.53f);
        FindObjectOfType<AudioManager>().StopPlaying("walkEffect");
    }
    private IEnumerator soundWalkBoss()
    {
        FindObjectOfType<AudioManager>().Play("walkEffect");
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<AudioManager>().StopPlaying("walkEffect");
    }

    private IEnumerator finalTransition(){
            fadeIn();
            yield return new WaitForSeconds(fadeTime);   
            Camera.main.GetComponent<MainCamera>().transitionPlayer = true;
            Camera.main.transform.position= target.transform.position;
            player.gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).GetComponent<Animator>().Play("finalTransition");
            StartCoroutine("soundWalk");
            manager.GetComponent<Animator>().Play("cameraMoov");
            canvas.transform.GetChild(0).gameObject.SetActive(false);
            canvas.transform.GetChild(1).gameObject.SetActive(false);
            canvas.transform.GetChild(2).gameObject.SetActive(false);
            fadeOut();
             yield return new WaitForSeconds(5);
        FindObjectOfType<AudioManager>().Play("effect1");
        GetComponent<Animator>().Play("boss"); 
        yield return new WaitForSeconds(3);
        FindObjectOfType<AudioManager>().Play("effect3");
        yield return new WaitForSeconds(2.55f); 
        StartCoroutine("soundWalkBoss");
        yield return new WaitForSeconds(2.45f);
            textPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.35f); 
        FindObjectOfType<AudioManager>().Play("effect2");
        flash.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.95f);
        canvas.GetComponent<Animator>().Play("flash");
        textCredits.gameObject.SetActive(true);

    }



    // Update is called once per frame
    void Update()
    {
        
    }
    void OnGUI()
    {
        if (!start)
        {
            return;
        }
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);

        Texture2D texture;
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.black);
        texture.Apply();
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
        if (isFadeIn)
        {
            alpha = Mathf.Lerp(alpha, 1.1f, fadeTime * Time.deltaTime);
        }
        else
        {
            alpha = Mathf.Lerp(alpha, -0.1f, fadeTime * Time.deltaTime);
            if (alpha < 0) start = false;
        }
    }
    void fadeIn()
    {
        start = true;
        isFadeIn = true;
    }
    void fadeOut()
    {
        isFadeIn = false;
    }
}
