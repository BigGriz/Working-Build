using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    [Header("Required Fields")]   
    public Sprite empty;
    public Sprite full;

    // Tracking Rubbish & Collection
    PlayerMovement trashPanda;
    [HideInInspector] public bool inRange;
    [HideInInspector] public List<Rubbish> rubbish;

    #region Setup
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    #endregion setup
    #region Callbacks
    private void Start()
    {       
        CallbackHandler.instance.takeTrash += TakeTrash;
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.takeTrash -= TakeTrash;
    }
    #endregion Callbacks

    public void TakeTrash()
    {
        // Have Trash
        if (rubbish.Count > 0)
        {
            // Give Trashpanda Last Item In
            trashPanda.carriedItem = rubbish[rubbish.Count - 1];
            trashPanda.carriedItem.Setup();
            trashPanda.carriedItem.CarryMe(trashPanda.transform);
            // Remove from Trash List & Check if any Left
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

    #region Triggers
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
    #endregion Triggers
}
