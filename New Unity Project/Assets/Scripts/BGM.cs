using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    #region Setup
    public AudioSource[] audio;
    int currentID;
    private void Awake()
    {
        audio = GetComponents<AudioSource>();        
    }
    #endregion Setup

    public bool final;

    public void PlayChaseMusic(bool _inChase)
    {
        if (!audio[1].isPlaying || !_inChase)
        {
            // Play Sounds
            audio[1].Stop();
            audio[1].Play();
        }
        audio[1].volume = 1.0f;
        // Fadeout BGM
        StartCoroutine(FadeOut(0.1f, 0));
    }

    public void FadeToBGM(float _time)
    {
        // Fadeout Chase
        StartCoroutine(FadeOut(_time, 1));
        if (!final)
        {
            // Fadein BGM
            StartCoroutine(FadeIn(_time, 0));
        }
    }

    public void FinalFadeOut()
    {
        // Fadeout BGM
        StartCoroutine(FadeOut(3.0f, 0));
    }

    public IEnumerator FadeOut(float FadeTime, int _source)
    {
        float startVolume = audio[_source].volume;

        while (audio[_source].volume > 0)
        {
            audio[_source].volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
    }

    public IEnumerator FadeIn(float FadeTime, int _source)
    {
        float startVolume = audio[_source].volume;

        while (audio[_source].volume < 1.0f)
        {
            audio[_source].volume += (1.0f - startVolume) * Time.deltaTime / FadeTime;

            yield return null;
        }
        
    }
}
