using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Spawn Timers
    float timer = 61.0f;
    float trashTimer = 20.0f;
    bool playSound;

    #region Setup
    TMPro.TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        playSound = false;
    }
    #endregion Setup

    // Update is called once per frame
    void Update()
    {
        // Stop Playing Audio & Pause Count while Paused
        if (CallbackHandler.instance.globalInfo.gamePaused)
        {
            GetComponent<AudioSource>().Stop();
            return;
        }

        // Play Countdown
        if (timer <= 15.0f && !playSound)
        {
            playSound = true;
            GetComponent<AudioSource>().Play();
            AudioController.instance.FinalFadeOut();
        }
        // Spawn Trash every X Seconds - Set Text of Timer
        if (timer >= 1.0f)
        {
            timer -= Time.deltaTime;
            trashTimer -= Time.deltaTime;
            if (trashTimer <= 0)
            {
                CallbackHandler.instance.SpawnTrash();
                trashTimer = 30.0f / CallbackHandler.instance.globalInfo.lockdownLevel;
            }
            text.SetText(Mathf.FloorToInt(timer).ToString());
        }
        // End Game
        else
        {
            text.SetText(Mathf.FloorToInt(timer).ToString());
            // Call end of game
            CallbackHandler.instance.EndGame();              
        }        
    }
}
