using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{

    AudioClip clip;
    void Start()
    {
        clip = gameObject.GetComponent<AudioSource>().clip;
    }

    public void Play()
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
