using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Sound : MonoBehaviour
{
    public AudioSource source;
    public Sound sound;

    public void PlaySound()
    {
        source.volume = sound.volume;
        source.clip = sound.clip;
        source.pitch = sound.pitch;
        source.Play();
    }
}
