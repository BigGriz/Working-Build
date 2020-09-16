using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    #region Setup
    SpriteRenderer spriteRenderer;
    Color color;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
    }
    #endregion Setup

    public int objStage;

    // Update is called once per frame
    void Update()
    {
        // Glow Objective
        if (CallbackHandler.instance.globalInfo.objStage == objStage)
        {
            color = new Color(color.r, color.g, color.b, Mathf.PingPong(Time.time, 0.5f));
            spriteRenderer.color = color;
        }
        else
        {
            color = new Color(color.r, color.g, color.b, 0.0f);
            spriteRenderer.color = color;
        }
    }
}
