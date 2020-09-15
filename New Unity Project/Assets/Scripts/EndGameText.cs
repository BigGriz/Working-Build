using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameText : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Start()
    {
        CallbackHandler.instance.setGGText += SetText;
    }

    private void OnDestroy()
    {
        CallbackHandler.instance.setGGText -= SetText;
    }

    public void SetText(string _gg)
    {
        text.SetText(_gg.ToString());
    }
}
