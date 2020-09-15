using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    // Object in front of character
    private GameObject obj;
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
                spriteRenderer.sortingOrder = 0;
                if (pm && pm.carriedItem)
                {
                    pm.carriedItem.SetOrder(0);
                }

                obj.GetComponent<SpriteRenderer>().sharedMaterial.SetVector("_FadeOrigin", transform.position);
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
                    pm.carriedItem.SetOrder(1);
                }
                spriteRenderer.sortingOrder = 1;
            }
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
