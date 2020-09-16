using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    #region Setup
    TMPro.TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }
    #endregion Setup

    private void Start()
    {
        // Set Startup Text
        text.SetText("LOCKDOWN LEVEL " + CallbackHandler.instance.globalInfo.lockdownLevel.ToString());
    }

    public void StartUp()
    {
        CallbackHandler.instance.globalInfo.gamePaused = false;
    }
}
