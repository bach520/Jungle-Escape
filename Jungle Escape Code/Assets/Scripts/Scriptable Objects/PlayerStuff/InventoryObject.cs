using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Object", menuName = "Inventory/New Inventory")]
public class InventoryObject : ScriptableObject
{
    #region variables
    [SerializeField] private Item[] playerEquipment = new Item[4];
    [SerializeField] int collectables;
    [SerializeField] int darts;
    WeaponBase weaponUsed = null;
    [SerializeField] private int score;
    #endregion

    private void Awake() {
        weaponUsed = (WeaponBase)playerEquipment[0];
    }

    public void AddItem(Item _item)
    {
        switch(_item.GetItemType())
        {
            case ItemType.WEAPON:
            {
                if(_item.GetID() == 1 && playerEquipment[0] == null)
                {
                    playerEquipment[0] = _item;
                }
                else if(_item.GetID() == 2 && playerEquipment[1] == null)
                {
                    playerEquipment[1] = _item;
                }
                else
                {
                    Debug.Log("Add parts");
                    collectables += _item.GetParts();
                }
                break;
            }
            case ItemType.AMMO:
            {
                Debug.Log("Picked up darts");
                darts += 20;
                break;
            }
            case ItemType.HEALING:
            {
                playerEquipment[3] = _item;
                break;
            }
            case ItemType.COIN:
            {
                collectables += 1;
                break;
            }
        }
    }

    public bool SlotEmpty(int i)
    {
        if(playerEquipment[i])
        {
            return false;
        }
        return true;
    }

    public Item GetItem(int i)
    {
        return playerEquipment[i];
    }

    public void RemoveItem(int i)
    {
        playerEquipment[i] = null;
    }

    #region get/set
    public int TotalDarts{get{return darts;} set{darts = value;}}
    public int TotalCollectables{get{return collectables;} set{collectables = value;}}
    public Item[] ItemsEquipped{get{return playerEquipment;}}
    public WeaponBase WeaponUsed{get{return weaponUsed;} set{weaponUsed = value;}}
    public int PlayerScore{get{return score;} set{score = value;}}
    #endregion
}