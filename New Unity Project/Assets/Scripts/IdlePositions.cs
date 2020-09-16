using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePositions : MonoBehaviour
{
    [HideInInspector] public List<Transform> idlePositions;

    #region Callbacks
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform n in transform)
        {
            idlePositions.Add(n);
        }
    }
    #endregion Callbacks

    public Transform GetIdlePosition()
    {
        int rand = Random.Range(0, idlePositions.Count);
        return (idlePositions[rand]);
    }
}
