using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable,
    Resource
}
public enum ConsumableType
{
    Health,
    Hunger,
    AddSpeed
}

[System.Serializable]
public class ItemDataConsumbale
    {
    public ConsumableType type;
    public float value;
    }

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]




public class ItemData :  ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;


    [Header("Consumable")]
    public ItemDataConsumbale[] consumables;
}
