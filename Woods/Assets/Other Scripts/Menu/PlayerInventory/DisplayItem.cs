using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItem : MonoBehaviour {

    public new string name;
    public string description;
    public string itemType;
    public int buyPrice;
    public int sellPrice;
    public Sprite itemImage;

    public int itemCount;

    public bool isEquipment = false;

    //FOR EQUIPMENTS
    public int lvl;
    public string abiType;  //Maybe add in the future
    public int abiAmt;

    //FOR WEAPONS
    public int atk;

    //FOR ARMOR
    public int def;

    //for items
    public float coolDown;

    //for healing items
    public string targetHeal;
    public int healAmt;

    private Text itemCountText;
    private Image imgHolder;

    public void DisplayItemGui(Item item)
    {
        name = item.name;
        description = item.description;
        itemType = item.itemType;
        buyPrice = item.buyPrice;
        sellPrice = item.sellPrice;
        //itemImage = item.image;

        imgHolder = GetComponent<Image>();
        imgHolder.sprite = item.image;

        itemCountText = transform.GetChild(0).GetComponent<Text>();
        itemCountText.text = itemCount.ToString();

        if(item.GetType().IsSubclassOf(typeof(Equipment)))
        {
            //Debug.Log("Its an equipment!");
            isEquipment = true;
        }
        if(item.GetType() == typeof(Weapon))
        {
            //Debug.Log("Its a weapon!");
            DisplayWeaponGui(item as Weapon);
        }
        else if(item.GetType() == typeof(Armor))
        {
            DisplayArmorGui(item as Armor);
        }
        else if(item.GetType() == typeof(HealingItem))
        {
            DisplayHealingItemGui(item as HealingItem);
        }
    }

    public void UpdateItemCount()
    {
        itemCountText.text = itemCount.ToString();
    }

    public void AddItemCount()
    {
        itemCount += 1;
        UpdateItemCount();
    }

    private void DisplayHealingItemGui(HealingItem healingItem)
    {
        coolDown = healingItem.coolDown;
        targetHeal = healingItem.targetHeal;
        healAmt = healingItem.healAmt;
    }

    private void DisplayWeaponGui(Weapon weap)
    {
        lvl = weap.lvl;
        atk = weap.atk;
        abiType = weap.abiType;
        abiAmt = weap.abiAmt;
    }

    private void DisplayArmorGui(Armor arm)
    {
        lvl = arm.lvl;
        def = arm.def;
        abiType = arm.abiType;
        abiAmt = arm.abiAmt;
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
}
