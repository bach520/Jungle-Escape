using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/New Weapon")]
public class WeaponBase : Item
{
#region variables
    [SerializeField] private Sprite image;

    [SerializeField] private float damage;

    [SerializeField] private float range;

    [SerializeField] private float atkSpeed;

    [SerializeField] private string weaponName;
#endregion

    private void Start() 
    {
        type = ItemType.WEAPON;
    }
    
#region get/set
    public float GetDamage() => damage;
    public float GetRange() => range;
    public float GetAtkSpeed() => atkSpeed;
    public Sprite GetImage() => image;
    public string GetName() => weaponName;
    public void SetAtkSpeed(float newSpeed) { atkSpeed = newSpeed; }
#endregion
}
