﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Data", order = 1)]
public class GlobalInfo : ScriptableObject
{
    public bool gamePaused;
    public int lockdownLevel;

    public List<Rubbish> rubbishList;
    public int trashCollected;
    public int totalTrash;

    public int objStage = 0;

    public void ClearList()
    {
        rubbishList.Clear();
        trashCollected = 0;
        gamePaused = true;
    }

    public void AddRubbish(Rubbish _rubbish)
    {
        rubbishList.Add(_rubbish);
    }

    public void RemoveRubbish(Rubbish _rubbish)
    {
        rubbishList.Remove(_rubbish);
    }

    public void CollectTrash()
    {
        trashCollected++;

        // EndGame once collected 3+ trash
        if (CheckWin())
        {
            CallbackHandler.instance.EndGame();
        }
    }

    public bool CheckWin()
    {
        return (trashCollected >= 3);
    }
}
