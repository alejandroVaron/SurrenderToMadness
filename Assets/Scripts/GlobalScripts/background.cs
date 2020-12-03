using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class background : MonoBehaviour
{
	public Sprite[] frames;
	public int fps = 10;
    // Use this for initialization
    void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		int index = (int)(Time.time * fps) % frames.Length;
		//GetComponent<Material>().mainTexture = frames[index];
		GetComponent<Image>().sprite = frames[index];
	}

}
