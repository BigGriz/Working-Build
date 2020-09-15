using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    Animator animator;
    TMPro.TextMeshProUGUI text;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Start()
    {
        //text.SetText(CallbackHandler.instance.globalInfo.currentLevel.ToString());
    }

    public void StartUp()
    {
        CallbackHandler.instance.globalInfo.gamePaused = false;
    }
}
