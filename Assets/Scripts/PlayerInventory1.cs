using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<string> keys = new List<string>();

    public bool HasKey(string keyId)
    {
        return keys.Contains(keyId);
    }

    public void AddKey(string keyId)
    {
        if (!keys.Contains(keyId))
        {
            keys.Add(keyId);
            Debug.Log("Clé ajoutée : " + keyId);
        }
    }

    public void RemoveKey(string keyId)
    {
        if (keys.Contains(keyId))
        {
            keys.Remove(keyId);
        }
    }
}
