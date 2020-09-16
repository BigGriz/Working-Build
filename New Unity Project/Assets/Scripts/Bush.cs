using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    #region Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player)
        {
            player.hidden = true;
        }

        Rubbish rubbish = collision.GetComponent<Rubbish>();
        if (rubbish)
        {
            rubbish.hidden = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player)
        {
            player.hidden = false;
        }

        Rubbish rubbish = collision.GetComponent<Rubbish>();
        if (rubbish)
        {
            rubbish.hidden = false;
        }
    }
    #endregion Triggers
}
