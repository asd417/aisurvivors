using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// refer to SoundManager.cs to see the usage guide, explaining how this sound system works

[System.Serializable] //  this is necesary to be visible in inspector
public class Sound {
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] // slider 0-1
    public float volume = 0.5f;
    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
}
