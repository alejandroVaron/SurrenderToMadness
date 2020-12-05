using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public float speed = 2f;
    Animator anim;
    Rigidbody2D rb2d;
    Vector2 mov;
    CircleCollider2D attackCollider;
    Life lifeBar;
    public GameObject wavePrefab;
    public GameObject minimap;
    float angle;
    Quaternion rotation;
    Queue<string> sentences;
    public TextMeshProUGUI displayText;
    public GameObject dialoguePanel;
    string activeSentence;
    public Dialogue dialogue;
    int count = 0;
    public GameObject canvas;
    bool inTransition = false;
    bool menu = false;
    int startMenu = 0;
    public bool inDialogue = false;

    void Start()
    {
        sentences = new Queue<string>();
        Camera.main.GetComponent<MainCamera>().updateLimit(-89, -22, -76.4f, -33.4f);
        anim = GetComponent<Animator>();
       StartCoroutine("introTransition");
        rb2d = GetComponent<Rigidbody2D>();
        attackCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
        attackCollider.enabled = false;
        lifeBar = gameObject.GetComponent<Life>();
        minimap.GetComponent<MinimapCamera>().updateLimit(-89, -22, -76.5f, -33.4f);
    }


    void Update()
    {
        if (!inTransition)
        {
            mov = new Vector2(
           Input.GetAxisRaw("Horizontal"),
           Input.GetAxisRaw("Vertical")
           );

            if (mov != Vector2.zero)
            {
                anim.SetFloat("movx", mov.x);
                anim.SetFloat("movy", mov.y);
                anim.SetBool("walking", true);
            }
            else
            {
                anim.SetBool("walking", false);
                //FindObjectOfType<AudioManager>().Play("PlayerWalkGrass");
            }



            if(Input.GetKeyDown(KeyCode.G))
            {
                startMenu++;
                menu = !menu;          
            }
            if (menu == true && startMenu > 0)
            {
                canvas.GetComponent<Animator>().Play("showPanelMenu");
            }
            else if(menu == false && startMenu > 0)
            {
                canvas.GetComponent<Animator>().Play("hidePanelMenu");
            }

            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            bool validate = stateInfo.IsName("player_attack");
            if (Input.GetKeyDown(KeyCode.Mouse0) && !validate && !inDialogue)
            {
                anim.SetTrigger("slash-trigger");
                FindObjectOfType<AudioManager>().Play("PlayerSwordSlash");
                //wave();
            }
            if (mov != Vector2.zero)
            {
                attackCollider.offset = new Vector2(mov.x / 3, mov.y / 3);
            }
            if (validate)
            {
                float playbackTime = stateInfo.normalizedTime;
                if (playbackTime > 0.4 && playbackTime < 0.7)
                {
                    attackCollider.enabled = true;
                }
                else
                {
                    attackCollider.enabled = false;
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (!inTransition)
        {
            rb2d.MovePosition(rb2d.position + mov * speed * Time.deltaTime);
            angle = Mathf.Atan2(
                            anim.GetFloat("movy"),
                            anim.GetFloat("movx")
                            ) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.GetChild(1).transform.rotation = rotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "AttackEnemy")
        {
            lifeBar.loseHealth(15);            
            FindObjectOfType<AudioManager>().Play("PlayerGotHit");
        }

       if(collision.tag == "AttackEnemyOrc")
        {
            FindObjectOfType<AudioManager>().Play("orcHit");
            lifeBar.loseHealth(15);
            FindObjectOfType<AudioManager>().Play("PlayerGotHit");
        }
    }
    public void wave()
    {
        float angle = Mathf.Atan2(anim.GetFloat("movy"), anim.GetFloat("movx")) * Mathf.Rad2Deg;
        GameObject wave = Instantiate(wavePrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward)
            );

        Wave obj = wave.GetComponent<Wave>();
        obj.mov.x = anim.GetFloat("movx");
        obj.mov.y = anim.GetFloat("movy");
    }

    IEnumerator introTransition()
    {
        inTransition = true;
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        anim.Play("Player-Exclamation");
        yield return new WaitForSeconds(4.1f);
        startDialogue();
        dialoguePanel.SetActive(true);
        dialoguePanel.transform.GetChild(1).gameObject.SetActive(true);
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
            dialoguePanel.SetActive(false);
            return;
        }
        activeSentence = sentences.Dequeue();
        displayText.text = activeSentence;
        StopAllCoroutines();
        StartCoroutine(typeTheSentence(activeSentence));
    }

    IEnumerator typeTheSentence(string sentence)
    {
        displayText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            FindObjectOfType<AudioManager>().Play("Typing");
            displayText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        if (count == 0)
        {
            yield return new WaitForSeconds(2);
            count++;
            dialoguePanel.transform.GetChild(1).gameObject.SetActive(false);
            dialoguePanel.transform.GetChild(2).gameObject.SetActive(true);
            displayNextSentence();
        }
        else
        {
            yield return new WaitForSeconds(1.6f);
            dialoguePanel.SetActive(false);
            canvas.transform.GetChild(0).gameObject.SetActive(true);
            canvas.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().fillAmount = 100 / 100;
            canvas.transform.GetChild(1).gameObject.SetActive(true);
            canvas.transform.GetChild(2).gameObject.SetActive(true);
            inTransition = false;
        }

}
}


