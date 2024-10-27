using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound : MonoBehaviour
{
    public new string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 0.5f;
    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
}
