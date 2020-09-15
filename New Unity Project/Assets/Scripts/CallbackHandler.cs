using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    }
    #endregion Singleton

    public GlobalInfo globalInfo;

    private void Start()
    {
        Invoke("StartUpCalls", 0.05f);
    }

    public void StartUpCalls()
    {
        SpawnTrash();
        SetTrashText();
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
        // TEMP RESTART
        Invoke("NextLevel", 2.5f);
    }

    public void NextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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
