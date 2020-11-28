using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Warp : MonoBehaviour
{
    public float x, y, dx, dy;
    public GameObject target;
    public GameObject minimap;
    bool start = false;
    bool isFadeIn = false;
    float alpha = 0;
    float fadeTime = 1f;
    GameObject area;
    public string targetMapName;
    // Start is called before the first frame update

    void Awake()
    {
        Assert.IsNotNull(target);
        area = GameObject.FindGameObjectWithTag("Area");
        //GetComponent<SpriteRenderer>().enabled = false;
        //transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<player>().enabled = false;
            other.GetComponent<Animator>().enabled = false;
            fadeIn();
            yield return new WaitForSeconds(fadeTime);
            other.transform.position = target.transform.position;
          
            Camera.main.GetComponent<MainCamera>().updateLimit(x, y, dx, dy);
            minimap.GetComponent<MainCamera>().updateLimit(x, y, dx, dy);
            fadeOut();
            other.GetComponent<player>().enabled = true;
            other.GetComponent<Animator>().enabled = true;
            StartCoroutine(area.GetComponent<Area>().showText(targetMapName));
        }
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
            alpha = Mathf.Lerp(alpha, 1.1f, fadeTime*Time.deltaTime);
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
