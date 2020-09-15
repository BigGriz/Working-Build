using System.Collections;
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

    public void PlayChase()
    {
        bgm.PlayChaseMusic();
    }

    public void PlaySFX(int _id)
    {
        sfx.PlayTrack(_id);
    }

    public void FadeToBGM()
    {
        bgm.FadeToBGM(3.0f);
    }
    public void QuickFadeToBGM()
    {
        bgm.FadeToBGM(0.1f);
    }
}
