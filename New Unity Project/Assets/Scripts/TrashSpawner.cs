using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [Header("Required Fields")]
    public GameObject rubbishPrefab;

    [HideInInspector] public List<Transform> spawnPositions;

    #region Callbacks
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform n in transform)
        {
            spawnPositions.Add(n);
        }

        CallbackHandler.instance.spawnTrash += SpawnRubbish;
    }

    private void OnDestroy()
    {
        CallbackHandler.instance.spawnTrash -= SpawnRubbish;
    }
    #endregion Callbacks

    public void SpawnRubbish()
    {
        int rand = Random.Range(0, spawnPositions.Count);
        Instantiate(rubbishPrefab, spawnPositions[rand].position, Quaternion.identity);
    }
}
