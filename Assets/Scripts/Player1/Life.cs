using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public Image fillBar;
    public float health;
    bool start = false;
    bool isFadeIn = false;
    float alpha = 0;
    float fadeTime = 1f;
    public Vector3 target;
    public void Start()
    {
        fillBar.fillAmount = health / 100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Heart")
        {
            health += 25;
            fillBar.fillAmount = health / 100;
        }
    }

    public void loseHealth(int value)
    {
        health -= value;
        fillBar.fillAmount = health / 100;
        if (health <= 0)
        {
            StartCoroutine("transition");
            return;
        }
      
    }
    IEnumerator transition()
    {
        GetComponentInParent<player>().GetComponent<Animator>().Play("player_death");
        GetComponentInParent<player>().GetComponent<player>().enabled = false;
        GetComponentInParent<player>().tag = "Death";
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        Camera.main.GetComponent<MainCamera>().player_death();
        yield return new WaitForSeconds(3f);
        fadeIn();
        yield return new WaitForSeconds(fadeTime);
        transform.position = new Vector3(target.x, target.y, 0);
        fadeOut();
        yield return new WaitForSeconds(0.8f);
        GetComponent<Animator>().Play("Anim1");
        yield return new WaitForSeconds(1);
        health = 100;
        fillBar.fillAmount = health/100;
        GetComponentInParent<player>().GetComponent<player>().enabled = true;
        GetComponentInParent<player>().GetComponent<Animator>().Play("player_idle");
        GetComponentInParent<player>().tag = "Player";
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
