using System;
using System.Collections;
using UnityEngine;

/* Usage Guide -> can use these methods in other scripts:

- begin in Start() {
    SoundManager.instance.Play("sound1"); 
}

- Adjust volume (e.g., lower music during other sfx)
    SoundManager.instance.SetVolume("sound1", 0.3f);

- Stop a sound effect or music
    SoundManager.instance.Stop("sound1");

- Stop all sounds (useful for scene changes)
    SoundManager.instance.StopAllSounds();

- Fade out sound over 2 seconds
    SoundManager.instance.FadeOut("sound1", 2f);

- Fade in a new sound over 2 seconds
    SoundManager.instance.FadeIn("sound2", 2f);

- Crossfade from sound1 to sound2
    SoundManager.instance.Crossfade("sound1", "sound2", 2f);

Note: configure `loop = true` for repeating--see Sound.cs */

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // Array to hold all sounds (effects and music)
    public Sound[] sounds;

    private void Awake()
    {
        // Singleton pattern: ensures only one instance of SoundManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy this GameObject when loading a new scene
            Debug.Log("SoundManager instance initialized.");

        }
        else
        {
            Destroy(gameObject); // Destroy duplicate SoundManagers
            Debug.Log("Duplicate SoundManager detected and destroyed.");
            return;
        }

        // Initialize each Sound's AudioSource
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>(); // Create AudioSource
            s.source.clip = s.clip;        // Assign audio clip
            s.source.volume = s.volume;    // Set initial volume
            s.source.loop = s.loop;        // Set looping option for music or effects
        }
    }

    // Method to play a sound by its name
    public void Play(string soundName)
    {
        // Debug.Log(soundName); // testing
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s != null)
        {
            s.source.Play(); // Start playing the sound
            // If the sound should loop, ensure it's set to true
            s.source.loop = s.loop; 
        }
        else
        {
            Debug.LogWarning($"Sound: {soundName} not found!"); // Error message if sound not found
        }
    }

    // Method to stop a sound by its name
    public void Stop(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s != null)
        {
            s.source.Stop(); // Stop playing the sound
            // s.source.volume = 0; // Set volume to 0 for safety

        }
        else
        {
            Debug.LogWarning($"Sound: {soundName} not found!"); // Error message if sound not found
        }
    }

    // Method to stop all sounds (useful for each scene change to ensure no sfx continuations)
    public void StopAllSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop(); // Stop each sound
        }
    }

    // Method to set the volume of a sound
    public void SetVolume(string soundName, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s != null)
        {
            s.source.volume = Mathf.Clamp(volume, 0f, 1f); // Set volume and clamp between 0 and 1
        }
        else
        {
            Debug.LogWarning($"Sound: {soundName} not found!"); // Error message if sound not found
        }
    }

    // Method to fade out a sound gradually over a specified duration
    public void FadeOut(string soundName, float duration)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s != null)
        {
            StartCoroutine(FadeOutCoroutine(s, duration)); // Start the fade-out coroutine
        }
    }

    // Method to fade in a sound gradually over a specified duration
    public void FadeIn(string soundName, float duration)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s != null)
        {
            StartCoroutine(FadeInCoroutine(s, duration)); // Start the fade-in coroutine
        }
    }

    // Method to crossfade between two sounds
    public void Crossfade(string soundOut, string soundIn, float duration)
    {
        FadeOut(soundOut, duration); // Fade out the first sound
        FadeIn(soundIn, duration);   // Fade in the second sound
    }

    // Coroutine for smoothly fading out a sound
    private IEnumerator FadeOutCoroutine(Sound sound, float duration)
    {
        float startVolume = sound.source.volume; // Store the current volume to calculate fade

        // Gradually reduce the volume over the specified duration
        while (sound.source.volume > 0)
        {
            sound.source.volume -= startVolume * Time.deltaTime / duration;
            yield return null; // Wait for the next frame
        }

        sound.source.Stop();             // Stop the sound after fade-out completes
        sound.source.volume = 0f;        // Ensure volume is reset to 0 for future use
    }

    // Coroutine for smoothly fading in a sound
    private IEnumerator FadeInCoroutine(Sound sound, float duration)
    {
        sound.source.volume = 0f;    // Start volume at 0 for fade-in effect
        sound.source.Play();         // Start playing the sound

        // Gradually increase the volume over the specified duration
        while (sound.source.volume < sound.volume)
        {
            sound.source.volume += Time.deltaTime / duration; // Increase volume per frame
            yield return null; // Wait for the next frame
        }

        sound.source.volume = sound.volume; // Ensure volume is set to the target value at the end
    }
}
