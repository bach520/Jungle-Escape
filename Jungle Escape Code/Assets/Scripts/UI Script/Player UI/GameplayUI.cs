using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUI : MonoBehaviour
{
#region player info variables
    [SerializeField] private InventoryObject playerInventory;

    [SerializeField] private StatsObject playerStats;

#endregion

#region UI variables
    [SerializeField] private GameObject spearButton;

    [SerializeField] private GameObject blowGunButton;

    [SerializeField] private GameObject thirdWeapon;

    [SerializeField] private GameObject healingItemButton;

    [SerializeField] private GameObject collectable;

    [SerializeField] private TextMeshProUGUI Darts;

    [SerializeField] private TextMeshProUGUI numCollectables;

    [SerializeField] private Slider healthbar;
#endregion

   public void Start() 
   {
       CheckEquipment();
       CheckNumbers();
   }
   
   private void Update() 
   {
       CheckEquipment();
       CheckNumbers();
   }

   private void CheckEquipment()
   {
       if(playerInventory.GetItem(1) != null)
       {
           blowGunButton.SetActive(true);
           Darts.text = playerInventory.TotalDarts.ToString();
       }
       else
       {
           blowGunButton.SetActive(false);
           Darts.text = "";
       }
       if(playerInventory.GetItem(2) != null)
       {
            thirdWeapon.SetActive(true);
       }
       else
       {
            thirdWeapon.SetActive(false);
       }
       if(playerInventory.WeaponUsed.GetName() == "Spear")
       {
            spearButton.GetComponent<Button>().interactable = true;
       }
       else if(playerInventory.WeaponUsed.GetName() == "BlowGun")
       {
            spearButton.GetComponent<Button>().interactable = false;
            blowGunButton.GetComponent<Button>().interactable = true;
       }
       if(playerInventory.ItemsEquipped[3] != null)
       {
           healingItemButton.GetComponent<Button>().interactable = true;
       }
       else
       {
           healingItemButton.GetComponent<Button>().interactable = false;
       }
   }
   private void CheckNumbers()
   {
       healthbar.value = playerStats.GetCurrentHealth() / playerStats.GetMaxHealth();
       numCollectables.text = playerInventory.TotalCollectables.ToString();
   }
}
