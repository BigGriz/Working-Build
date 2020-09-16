using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    public bool inRange;
    PlayerMovement trashPanda;
    SpriteRenderer spriteRenderer;

    public Sprite empty;
    public Sprite full;


    public List<Rubbish> rubbish;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            if (rubbish.Count == 0)
            {
                spriteRenderer.sprite = empty;
            }
        }
    }

    public void AddRubbish(Rubbish _rubbish)
    {
        rubbish.Add(_rubbish);
        _rubbish.CleanUp();
        spriteRenderer.sprite = full;
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
