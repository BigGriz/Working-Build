using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CallbackHandler : MonoBehaviour
{
    #region Singleton
    public static CallbackHandler instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Callback Handler Exists!");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        globalInfo.ClearList();
        globalInfo.lockdownLevel++;
    }
    #endregion Singleton

    public GlobalInfo globalInfo;
    public Fader fader;

    private void Start()
    {
        Invoke("StartUpCalls", 0.05f);
    }

    public void StartUpCalls()
    {
        for (int i = 4; i > globalInfo.lockdownLevel; i--)
        {
            SpawnTrash();
        }
        SetTrashText();
        globalInfo.objStage = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnTrash();
        }
    }

    public void EndGame()
    {
        SetGGText(globalInfo.CheckWin());
        globalInfo.gamePaused = true;

        Invoke("Transition", 3.0f);
    }

    public void Transition()
    {
        AudioController.instance.bgm.final = true;
        AudioController.instance.QuickFadeToBGM();

        if (globalInfo.CheckWin())
        {
            switch (globalInfo.lockdownLevel + 1)
            {
                case 1:
                    {
                        fader.ChangeLevel("Level1");
                        break;
                    }
                case 2:
                    {
                        fader.ChangeLevel("Level2");
                        break;
                    }
                case 3:
                    {
                        fader.ChangeLevel("Level3");
                        break;
                    }
                case 4:
                    {
                        fader.ChangeLevel("Level4");
                        break;
                    }
                case 5:
                    {
                        fader.ChangeLevel("End");
                        break;
                    }
            }
        }
        else
        {
            fader.ChangeLevel("Lose");
        }
    }

    public event Action spawnTrash;
    public void SpawnTrash()
    {
        if (spawnTrash != null)
        {
            spawnTrash();
        }
    }

    public event Action takeTrash;
    public void TakeTrash()
    {
        if (takeTrash != null)
        {
            takeTrash();
        }
    }

    public event Action<Rubbish> storeTrash;
    public void StoreTrash(Rubbish _rubbish)
    {
        if (storeTrash != null)
        {
            globalInfo.objStage = 2;
            
            storeTrash(_rubbish);
            SetTrashText();
        }
    }

    public event Action<int> setTrashText;
    public void SetTrashText()
    {
        if (setTrashText != null)
        {
            setTrashText(globalInfo.trashCollected);
        }
    }

    public event Action<string> setGGText;
    public void SetGGText(bool _GG)
    {
        if (setGGText != null)
        {
            if (_GG)
            {
                setGGText("You Win!");
            }
            else
            {
                setGGText("You Lose!");
            }
        }
    }
}
