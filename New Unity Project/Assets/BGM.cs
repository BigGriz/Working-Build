using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    #region Setup
    AudioSource audio;
    int currentID;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();        
    }
    #endregion Setup

    public AudioClip[] bgmTracks;

    public void PlayTrack(int _id)
    {
        if (currentID != _id)
        {
            currentID = _id;
            audio.Stop();
            audio.PlayOneShot(bgmTracks[_id]);
        }


    }

    public void FadeToBGM(float _time)
    {
        StartCoroutine(FadeOut(_time));
    }

    public IEnumerator FadeOut(float FadeTime)
    {
        float startVolume = audio.volume;

        while (audio.volume > 0)
        {
            audio.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audio.Stop();
        audio.volume = startVolume;

        PlayTrack(0);
    }
}
