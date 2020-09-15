using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    #region Setup
    AudioSource audio;
    int id;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    #endregion Setup

    public AudioClip[] sfxTracks;

    public void PlayTrack(int _id)
    {
        if (!audio.isPlaying || id != _id)
        {
            id = _id;
            audio.PlayOneShot(sfxTracks[_id]);
        }
    }
}
