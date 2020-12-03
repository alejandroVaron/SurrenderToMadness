using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float smoothTime = 3f;
    Transform target;
    //Vector2 velocity;
    Animator anim;
    private float tlx, tly, brx, bry;
    private Vector3 velocity;
    Vector2 velocitys;
    Vector3 positionActual;
    public GameObject transitionIntro;
    public GameObject canva;
    public GameObject area;
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("transition");
        StartCoroutine(area.GetComponent<Area>().showText("Hogar"));
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float posx = Mathf.Round( Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocitys.x, 0.3f) * 100) / 100;
        float posy = Mathf.Round(Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocitys.y, 0.3f) * 100) / 100;

        transform.position = new Vector3(
            Mathf.Clamp(posx, tlx, brx) , 
            Mathf.Clamp(posy, bry, tly), 
            transform.position.y-100
            );
        positionActual = transform.position;
    }

    public void updateLimit(float x, float y, float z, float  u)
    {
        float cameraSize = Camera.main.orthographicSize;
        tlx = x + cameraSize;
        tly = y - cameraSize;
        brx = z - cameraSize;
        bry = u + cameraSize;
        fastMove();

    }
    public void fastMove()
    {
        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            target.position.z
            );
    }
    public void player_death()
    {
        anim.Play("camera_zoom");
    }
    public Vector3 getPositionActual()
    {
        return positionActual;
    }
    IEnumerator transition()
    {
        yield return new WaitForSeconds(0.5f);

        canva.GetComponent<Animator>().Play("transitionIntro");
        yield return new WaitForSeconds(0.5f);
        transitionIntro.SetActive(false);
    }
}
