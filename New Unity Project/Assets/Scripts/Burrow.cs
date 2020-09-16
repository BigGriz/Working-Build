using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burrow : MonoBehaviour
{
    #region Callbacks
    private void Start()
    {
        CallbackHandler.instance.storeTrash += StoreTrash;
    }

    private void OnDestroy()
    {
        CallbackHandler.instance.storeTrash -= StoreTrash;
    }
    #endregion Callbacks

    public void StoreTrash(Rubbish _rubbish)
    {
        CallbackHandler.instance.globalInfo.CollectTrash();
        _rubbish.CleanUp();
        Destroy(_rubbish.gameObject);
    }

    #region Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player)
        {
            player.atBurrow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player)
        {
            player.atBurrow = false;
        }
    }
    #endregion Triggers
}
