using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashText : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Start()
    {
        CallbackHandler.instance.setTrashText += SetText;
    }

    private void OnDestroy()
    {
        CallbackHandler.instance.setTrashText -= SetText;
    }

    public void SetText(int _trash)
    {
        text.SetText(_trash.ToString());
    }
}
