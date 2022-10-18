using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    WEAPON,
    AMMO,
    HEALING,
    UPGRADE,
    COIN
}

public abstract class Item : ScriptableObject
{
    [SerializeField]
    int ID;
    [SerializeField]
    int coins;
    [SerializeField]
    protected ItemType type;

    public ItemType GetItemType() => type;
    public int GetID() => ID;
    public int GetParts() => coins;
}
