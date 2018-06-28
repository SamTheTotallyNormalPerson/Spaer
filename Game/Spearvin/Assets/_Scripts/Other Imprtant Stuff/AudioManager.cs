using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Soundy[] sounds;

    void Awake()
    {
        foreach (Soundy s in sounds)
        {
        s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;


        }
    }
    private void Start()
    {
        Play("Test Music");
    }
    public void Play (string name)
    {
       Soundy s= Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}