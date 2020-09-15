using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    #region Setup
    AudioSource audio;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    #endregion Setup

    public AudioClip[] sfxTracks;

    public void PlayTrack(int _id)
    {
        audio.PlayOneShot(sfxTracks[_id]);
    }
}
