using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClip : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip audioClip;

    // Use this for initialization
    void Start () {
        audioSource.clip = audioClip;
        
    }

    public void playClip()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
