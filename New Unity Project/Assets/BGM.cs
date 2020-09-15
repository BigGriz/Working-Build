using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    #region Setup
    AudioSource audio;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();        
    }
    #endregion Setup

    public AudioClip[] bgmTracks;

    public void PlayTrack(int _id)
    {
        audio.PlayOneShot(bgmTracks[_id]);
    }
}
