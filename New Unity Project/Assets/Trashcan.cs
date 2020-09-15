using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    public bool inRange;
    PlayerMovement trashPanda;

    public List<Rubbish> rubbish;

    private void Start()
    {
        CallbackHandler.instance.takeTrash += TakeTrash;
    }

    private void OnDestroy()
    {
        CallbackHandler.instance.takeTrash -= TakeTrash;
    }

    public void TakeTrash()
    {
        // Have Trash
        if (rubbish.Count > 0)
        {
            trashPanda.carriedItem = rubbish[rubbish.Count - 1];
            trashPanda.carriedItem.Setup();
            trashPanda.carriedItem.CarryMe(trashPanda.transform);
            rubbish.Remove(trashPanda.carriedItem);
        }
    }

    public void AddRubbish(Rubbish _rubbish)
    {
        rubbish.Add(_rubbish);
        _rubbish.CleanUp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player)
        {
            player.inRange = true;
            trashPanda = player;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player)
        {
            player.inRange = false;
            trashPanda = null;
        }
    }
}
