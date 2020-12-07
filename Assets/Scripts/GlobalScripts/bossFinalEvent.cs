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
        if(collision.tag == "Player")
        {
            StartCoroutine("finalTransition");
        }
    }

    private IEnumerator finalTransition(){
            fadeIn();
            yield return new WaitForSeconds(fadeTime);   
            Camera.main.GetComponent<MainCamera>().transitionPlayer = true;
            Camera.main.transform.position= target.transform.position;
            transform.GetChild(0).GetComponent<Animator>().Play("finalTransition");
            manager.GetComponent<Animator>().Play("cameraMoov");
            canvas.transform.GetChild(0).gameObject.SetActive(false);
            canvas.transform.GetChild(1).gameObject.SetActive(false);
            canvas.transform.GetChild(2).gameObject.SetActive(false);
            fadeOut();
             yield return new WaitForSeconds(5);
            GetComponent<Animator>().Play("boss");
             yield return new WaitForSeconds(8);
            textPanel.gameObject.SetActive(true);
        flash.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.25f);
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
