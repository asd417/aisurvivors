using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// refer to SoundManager.cs to see the usage guide, explaining how this sound system works

[System.Serializable] //  this is necesary to be visible in inspector
public class Sound {
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] // slider from 0%-100% vol
    public float volume;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
