using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    #region Setup
    PlayerMovement player;
    Vector3 offset = new Vector3(0, 0, -10.0f);
    private void Start()
    {
        player = PlayerMovement.instance;
    }
    #endregion Setup

    // Update Camera Pos
    private void Update()
    {
        transform.position = player.transform.position + offset;
    }
}