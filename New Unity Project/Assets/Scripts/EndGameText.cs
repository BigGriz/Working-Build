using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameText : MonoBehaviour
{
    #region Setup
    TMPro.TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }
    #endregion Setup
    #region Callbacks
    private void Start()
    {
        CallbackHandler.instance.setGGText += SetText;
    }

    private void OnDestroy()
    {
        CallbackHandler.instance.setGGText -= SetText;
    }
    #endregion Callbacks

    public void SetText(string _gg)
    {
        text.SetText(_gg.ToString());
    }
}
