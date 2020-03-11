using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageItem : MonoBehaviour {

    private DisplayItem itemDetails;
    //public GameObject playerGo;
    public Player player;

    public GameObject equipmentGo;
    private GameObject weapSlot;
    private GameObject armSlot;
    private GameObject headSlot;

    public bool inShortcut;

    // Use this for initialization
    void Start()
    {
        itemDetails = GetComponent<DisplayItem>();
        //player = playerGo.GetComponent<Player>();
        //Debug.Log(player.name);

        weapSlot = equipmentGo.transform.GetChild(2).gameObject;
        armSlot = equipmentGo.transform.GetChild(0).gameObject;
        headSlot = equipmentGo.transform.GetChild(1).gameObject;
    }

    public void UseItemInShortcut()
    {
        UseItem();
    }

    public bool Equip()
    {
        if(itemDetails.itemType == "weapon")
        {
            EquipWeapon();
        }
        else
        {
            EquipArmor();
        }

        CheckForAbi();
        if(itemDetails.lvl > player.lvl)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void DecreaseItemCount()
    {
        itemDetails.itemCount -= 1;
        if(itemDetails.itemCount == 0)
        {
            Destroy(gameObject);
        }
        itemDetails.UpdateItemCount();
    }

    public void UseItem()
    {
        switch (itemDetails.targetHeal)
        {
            case "hp":
                player.RestoreHp(itemDetails.healAmt);
                break;
            case "mana":
                player.RestoreMana(itemDetails.healAmt);
                break;
        }
        DecreaseItemCount();
    }

    private void CheckForAbi()
    {
        if (itemDetails.abiType != null)
        {
            switch (itemDetails.abiType)
            {
                case "dmg":
                    player.dmg += itemDetails.abiAmt;
                    break;
                case "mdmg":
                    player.mdmg += itemDetails.abiAmt;
                    break;
                case "def":
                    player.def += itemDetails.abiAmt;
                    break;
                case "mdef":
                    player.mdef += itemDetails.abiAmt;
                    break;
                case "hp":
                    player.maxHp += itemDetails.abiAmt;
                    break;
            }
        }
    }

    private void EquipArmor()
    {
        GameObject currentEquipped;
        DisplayItem currentEquippedDetails;
        GameObject equipSlot = null;

        if (itemDetails.lvl > player.lvl)
        {
            Debug.Log("Level too low!");
            return;
        }

        if (itemDetails.itemType == "body")
        {
            equipSlot = armSlot;
        }else if(itemDetails.itemType == "head")
        {
            equipSlot = headSlot;
        }

        if (equipSlot.transform.childCount != 0) //alrdy has stuff equipped
        {
            currentEquipped = equipSlot.transform.GetChild(0).gameObject;

            currentEquippedDetails = currentEquipped.GetComponent<DisplayItem>();

            player.def -= currentEquippedDetails.def;

            currentEquipped.transform.SetParent(transform.parent);  //swap equip's position with mine
            currentEquipped.transform.position = currentEquipped.transform.parent.position;
        }
        
            transform.SetParent(equipSlot.transform);
            transform.position = equipSlot.transform.position;
            player.def += itemDetails.def;
        
    }

    private void EquipWeapon()
    {
        GameObject currentEquipped;
        DisplayItem currentEquippedDetails;

        if (itemDetails.lvl > player.lvl)
        {
            Debug.Log("Level too low!");
            return;
        }

        if (weapSlot.transform.childCount != 0) //alrdy has stuff equipped
        {
            currentEquipped = weapSlot.transform.GetChild(0).gameObject;
            currentEquippedDetails = currentEquipped.GetComponent<DisplayItem>();

            player.dmg -= currentEquippedDetails.atk;

            currentEquipped.transform.SetParent(transform.parent);  //swap equip's position with mine
            currentEquipped.transform.position = currentEquipped.transform.parent.position;
        }

            transform.SetParent(weapSlot.transform);
            transform.position = weapSlot.transform.position;
            player.dmg += itemDetails.atk;
    }

}
