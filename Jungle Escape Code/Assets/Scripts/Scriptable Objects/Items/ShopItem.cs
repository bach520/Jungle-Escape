using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum shopItemTarget
{
    ATTACKSPEED,
    HEALTH,
    RUNSPEED
}

[CreateAssetMenu(fileName = "New Shop Item",menuName = "Items/Shop Item")]
public class ShopItem : ScriptableObject
{
    [SerializeField] Sprite image;
    [SerializeField] WeaponBase weapon;
    [SerializeField] shopItemTarget target;
    [SerializeField] float increaseAmount;
    [SerializeField] string description;
    [SerializeField] int cost;
    [SerializeField] bool isSold;

    private void Awake()
    {
        IsSold = false;
    }

    public Sprite GetImage() => image;
    public shopItemTarget GetItemTarget() => target;
    public float GetIncreaseAmount() => increaseAmount;
    public string GetDescription() => description;
    public int GetCost() => cost;
    public WeaponBase GetWeapon() => weapon;
    public bool IsSold { get { return isSold; } set { isSold = value; } }
}
