﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    #region Singleton
    public static AudioController instance;
    BGM bgm;
    SFX sfx;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioController exists!");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        bgm = GetComponentInChildren<BGM>();
        sfx = GetComponentInChildren<SFX>();
    }
    #endregion Singleton

    public void PlayBGM(int _id)
    {
        bgm.PlayTrack(_id);
    }

    public void PlaySFX(int _id)
    {
        sfx.PlayTrack(_id);
    }
}
