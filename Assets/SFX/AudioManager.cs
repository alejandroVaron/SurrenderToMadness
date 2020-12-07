using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake ()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void StopPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //Debug.Log(sound+ " == "+ )
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        Debug.Log("Parar la canción: " + s.name);
        s.source.Stop();
    }


    public void stop()
    {
        GetComponent<AudioSource>().Stop();
    }

    // Update is called once per frame
    public void Play (string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
