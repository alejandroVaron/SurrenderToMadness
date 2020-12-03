using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPrincipal : MonoBehaviour
{
    bool start = false;
    bool isFadeIn = false;
    float alpha = 0;
    float fadeTime = 3f;
    public GameObject panel;
    Animator anim;
    public GameObject image;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startDificulty()
    {
        FindObjectOfType<AudioManager>().Play("EffectSelect");
        panel.transform.GetChild(0).gameObject.SetActive(false);
        panel.transform.GetChild(1).gameObject.SetActive(true);
    }
    public void startIntro()
    {
        FindObjectOfType<AudioManager>().Play("EffectSelect");
        StartCoroutine("loadScenes");
    }
    public void quitGame()
    {
        Application.Quit();
    }

    IEnumerator loadScenes()
    {
        anim.Play("LoadIntro");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Intro");
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
