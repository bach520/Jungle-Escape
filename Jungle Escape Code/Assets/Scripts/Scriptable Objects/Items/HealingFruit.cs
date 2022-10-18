using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Healing Fruit", menuName = "Items/Healing Fruit")]
public class HealingFruit : Item
{
    [SerializeField]
    int healAmount = 10;
    
    public float useItem()
    {
        return healAmount;
    }
}
