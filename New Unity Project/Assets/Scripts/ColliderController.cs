using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public CircleCollider2D collider;

    public void UpdateCol(Vector2 dir)
    {
        if (dir != Vector2.zero)
        collider.offset = dir * 0.4f;
    }
}
