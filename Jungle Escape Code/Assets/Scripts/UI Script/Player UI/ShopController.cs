using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour
{
    [SerializeField] StatsObject playerStats;
    [SerializeField] InventoryObject playerInventory;
    [SerializeField]
    private Transform layout;
    [SerializeField]
    private GameObject itemTemplate;
    [SerializeField] private List<ShopItem> itemInfo = new List<ShopItem>();
    List<Button> buyButton = new List<Button>();
    int coinAmount;

    private void Start()
    {
        coinAmount = playerInventory.TotalCollectables;
        transform.Find("coinAmount").GetComponent<TextMeshProUGUI>().SetText(coinAmount.ToString());

        for (int i = 0; i < itemInfo.Count; i++) // create new shop item button
        {
            CreateItemButton(itemInfo[i], i);
        }
        Destroy(itemTemplate); // destroy base template
    }
    void Update()
    {
        transform.Find("coinAmount").GetComponent<TextMeshProUGUI>().SetText(coinAmount.ToString());

        for (int i = 0; i < itemInfo.Count; i++) // set item button interactibility
        {
            if (coinAmount < itemInfo[i].GetCost() || itemInfo[i].IsSold == true)
            {
                buyButton[i].interactable = false;
            }
            else buyButton[i].interactable = true;
        }
    }

    private void CreateItemButton(ShopItem newItemInfo, int index)
    {
        GameObject newItemTemplate = Instantiate(itemTemplate, layout); // instantiate new item button inside panel

        newItemTemplate.transform.Find("image").GetComponent<Image>().sprite = newItemInfo.GetImage(); // set item image
        newItemTemplate.transform.Find("description").GetComponent<Text>().text = newItemInfo.GetDescription() + " +" + newItemInfo.GetIncreaseAmount(); // set item description
        newItemTemplate.transform.Find("cost").GetComponent<Text>().text = newItemInfo.GetCost().ToString(); // set item cost
        buyButton.Add(newItemTemplate.transform.Find("buyButton").GetComponent<Button>()); // add buyButton to buyButtons' list
        buyButton[index].onClick.AddListener(() => BuyItem(index)); // add event to onClick
        if (newItemInfo.IsSold) // if item is sold
        {
            buyButton[index].interactable = false; // disable buyButton
            buyButton[index].transform.Find("buyButtonText").GetComponent<TextMeshProUGUI>().SetText("SOLD!"); // set text "SOLD!"
        }
        else
        {
            buyButton[index].interactable = true; // enable buyButton
            buyButton[index].transform.Find("buyButtonText").GetComponent<TextMeshProUGUI>().SetText("BUY"); // set text "BUY"
        }
    }
    public void BuyItem(int index)
    {
        Debug.Log("Item at index " + index);

        coinAmount -= itemInfo[index].GetCost(); // decrease coin amount
        itemInfo[index].IsSold = true; // set isSold true

        buyButton[index].transform.Find("buyButtonText").GetComponent<TextMeshProUGUI>().SetText("SOLD!"); // set text "SOLD!"
        buyButton[index].interactable = false; // disable buyButton

        ApplyEffect(index, itemInfo[index].GetItemTarget(), itemInfo[index].GetIncreaseAmount()); // apply item effect
    }

    private void ApplyEffect(int index, shopItemTarget target, float effectAmount)
    {
        Debug.Log("apply effect");
        if (target == shopItemTarget.ATTACKSPEED)
        {
            WeaponBase weapon = itemInfo[index].GetWeapon();
            weapon.SetAtkSpeed(weapon.GetAtkSpeed() - effectAmount);
        }
        else if (target == shopItemTarget.HEALTH)
        {
            // increase health
            playerStats.MaxHealth += effectAmount;
        }
        else if (target == shopItemTarget.RUNSPEED)
        {
            // increase run speed
            playerStats.Speed += effectAmount;
        }
    }
}
