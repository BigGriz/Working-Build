using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burrow : MonoBehaviour
{
    public List<Rubbish> rubbish;

    private void Start()
    {
        CallbackHandler.instance.storeTrash += StoreTrash;
    }

    private void OnDestroy()
    {
        CallbackHandler.instance.storeTrash -= StoreTrash;
    }

    public void StoreTrash(Rubbish _rubbish)
    {
        rubbish.Add(_rubbish);
        _rubbish.CleanUp();
    }

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
}
