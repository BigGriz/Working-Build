using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timer = 61.0f;
    public float trashTimer = 20.0f;
    TMPro.TextMeshProUGUI text;

    public bool playSound;

    private void Awake()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        playSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CallbackHandler.instance.globalInfo.gamePaused)
        {
            GetComponent<AudioSource>().Stop();
            return;
        }

        if (timer <= 15.0f && !playSound)
        {
            playSound = true;
            GetComponent<AudioSource>().Play();
            AudioController.instance.FinalFadeOut();
        }

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
        else
        {
            text.SetText(Mathf.FloorToInt(timer).ToString());
            // Call end of game
            CallbackHandler.instance.EndGame();              
        }        
    }
}
