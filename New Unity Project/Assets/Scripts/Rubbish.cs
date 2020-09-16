using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubbish : MonoBehaviour
{
    [Header("Required Fields")]
    public Sprite[] sprites;
    public float dropAmount = 0.1f;
    // Private Trackers
    [HideInInspector] public Transform character;
    [HideInInspector] public bool carried;
    [HideInInspector] public bool hidden;
    [HideInInspector] public bool inBin;
    [HideInInspector] public Vector3 xOffset;

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
        // Update Pos if Carried
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
        inBin = false;

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

    // For Transparency Shader
    public void SetOrder(int _order)
    {
        spriteRenderer.sortingOrder = _order;
    }

    public void CleanUp()
    {
        character = null;
        carried = false;
        inBin = true;
        spriteRenderer.enabled = false;
        // Remove from Rubbish Tracking
        CallbackHandler.instance.globalInfo.RemoveRubbish(this);
    }

    public void Setup()
    {
        spriteRenderer.enabled = true;
        // Add to Rubbish Tracking
        CallbackHandler.instance.globalInfo.AddRubbish(this);
    }
}
