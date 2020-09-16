using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashText : MonoBehaviour
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
        CallbackHandler.instance.setTrashText += SetText;
    }

    private void OnDestroy()
    {
        CallbackHandler.instance.setTrashText -= SetText;
    }
    #endregion Callbacks

    public void SetText(int _trash)
    {
        text.SetText(_trash.ToString());
    }
}
