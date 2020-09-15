using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Data", order = 1)]
public class GlobalInfo : ScriptableObject
{
    public List<Rubbish> rubbishList;

    public void ClearList()
    {
        rubbishList.Clear();
    }

    public void AddRubbish(Rubbish _rubbish)
    {
        rubbishList.Add(_rubbish);
    }

    public void RemoveRubbish(Rubbish _rubbish)
    {
        rubbishList.Remove(_rubbish);
    }
}
