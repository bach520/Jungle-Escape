using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DartBox", menuName = "Items/Dart Box")]
public class DartBox : Item
{
    private void Awake()
    {
        type = ItemType.AMMO;
    }
}
