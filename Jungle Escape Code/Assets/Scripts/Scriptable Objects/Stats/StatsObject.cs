using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Block", menuName = "Stat Block/New Stat Block")]
public class StatsObject : ScriptableObject
{
    #region variables
    [SerializeField]
    private float currentHealth; // must be float to interact properly with UI health bar
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private Sprite characterImage;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int damage;
    public string characterName;
    public bool playAudio = false;
    public float TimeBetweenAttacks = 1.5f;
    #endregion

    #region get/set
    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
    public Sprite GetSprite() => characterImage;
    public float GetSpeed() => speed;
    public int GetDamage() => damage;
    public float MaxHealth{get{return maxHealth;} set{maxHealth = value;}}
    public float Speed{get{return speed;} set{speed = value;}}
    public void SetStats()
    {
        currentHealth = maxHealth;
    }

    #endregion

    public void TakeDamage(int damage)
    {
        currentHealth -= (float)damage;
        playAudio = true;    
    }

    public void AddHealth(float heal)
    {
        currentHealth += heal;
    }

}
