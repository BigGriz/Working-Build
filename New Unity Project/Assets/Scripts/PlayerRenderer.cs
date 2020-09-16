using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    // Object in front of character
    private GameObject obj;
    private PlayerRenderer character;
    [HideInInspector] public List<Collider2D> triggers;
    #region Setup
    private SpriteRenderer spriteRenderer;
    private PlayerMovement pm;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pm = GetComponent<PlayerMovement>();
    }
    #endregion Setup

    private void Update()
    {
        if (obj != null)
        {
            if (transform.position.y > obj.transform.position.y)
            {
                spriteRenderer.sortingOrder = 1;
                if (pm && pm.carriedItem)
                {
                    pm.carriedItem.SetOrder(1);
                }

                obj.GetComponent<SpriteRenderer>().sharedMaterial.SetVector("_FadeOrigin", transform.position);
            }
        }

        if (character != null)
        {
            if (transform.position.y > character.transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 1);
            }
        }
    }

    #region Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mask"))
        {
            triggers.Add(collision);
            obj = collision.gameObject;
        }

        if (collision.GetComponent<PlayerRenderer>())
        {
            character = collision.GetComponent<PlayerRenderer>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Mask"))
        {
            triggers.Remove(collision);
            if (triggers.Count == 0)
            {
                obj = null;
                if (pm && pm.carriedItem)
                {
                    pm.carriedItem.SetOrder(2);
                }
                spriteRenderer.sortingOrder = 2;
            }
        }

        if (collision.GetComponent<PlayerRenderer>())
        {
            character = null;
        }
    }
    #endregion Triggers

    public void AllowMovement()
    {
        AIMovement garbageMan = GetComponentInParent<AIMovement>();
        if (garbageMan)
        {
            garbageMan.AllowMovement();
        }
    }
}
