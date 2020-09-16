using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubbish : MonoBehaviour
{
    public Sprite[] sprites;

    public Transform character;
    public bool carried;
    public bool hidden;
    [Header("Required Fields")]
    public Vector3 xOffset;
    public float dropAmount = 0.1f;

    #region Setup
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        CallbackHandler.instance.globalInfo.AddRubbish(this);

        int rand = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[rand];
    }
    #endregion Setup

    private void Update()
    {
        if (carried)
        {
            if (character.GetComponent<SpriteRenderer>().flipX)
            {
                transform.position = character.transform.position + xOffset * -1;
            }
            else
            {
                transform.position = character.transform.position + xOffset;
            }
        }
    }

    public void CarryMe(Transform _character)
    {
        character = _character;
        carried = true;

        xOffset = _character.GetComponent<PlayerMovement>() ? xOffset = _character.GetComponent<PlayerMovement>().xOffset : Vector3.zero;

        if (GetComponentInChildren<Glow>())
        {
            CallbackHandler.instance.globalInfo.objStage = 1;
        }
    }

    public void DropMe()
    {
        character = null;
        carried = false;
        transform.Translate(-Vector3.up * dropAmount);
    }

    public void SetOrder(int _order)
    {
        spriteRenderer.sortingOrder = _order;
    }

    public void CleanUp()
    {
        CallbackHandler.instance.globalInfo.RemoveRubbish(this);
        spriteRenderer.enabled = false;
    }

    public void Setup()
    {
        CallbackHandler.instance.globalInfo.AddRubbish(this);
        spriteRenderer.enabled = true;
    }
}
