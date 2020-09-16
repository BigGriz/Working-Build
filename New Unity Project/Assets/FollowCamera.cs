using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0, 0, -10.0f);


    private void Update()
    {
        transform.position = player.transform.position + offset;
    }
}