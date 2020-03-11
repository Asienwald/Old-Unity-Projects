using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[System.Serializable]
public class Player : MonoBehaviour {

    //  PLAYER STATS
    public string name = "Player";
    public int lvl = 1;
    //public int exp = 0;
    public int lua = 0;
    public int currentHp = 100;
    public int maxHp = 100;
    public int currentMana = 100;
    public int maxMana = 100;
    public int dmg = 10;
    public int mdmg = 10;
    public int def = 5;
    public int mdef = 5;

    public int statPts = 0;
    public int str = 1;
    public int vit = 1;
    public int wis = 1;

    public int surplusExp;
    public int expToLvl;

    private void Awake()
    {
        name = "Player";
        lvl = 1;
        //exp = 0;
        lua = 0;
        currentHp = 100;
        maxHp = 100;
        currentMana = 100;
        maxMana = 100;
        dmg = 10;
        mdmg = 10;
        def = 5;
        mdef = 5;
        statPts = 5;
        str = 1;
        vit = 1;
        wis = 1;

        surplusExp = 0;
        expToLvl = lvl * (10 + lvl) - surplusExp;
    }

    private void LvlUp()
    {
        lvl += 1;
        dmg += lvl * 2;
        mdmg += lvl;
        def += lvl * 2;
        mdef += lvl * 2;
        maxHp += lvl * 10;
        currentHp = maxHp;
        currentMana = maxMana;
    }

    public void GetExp(int amt)
    {
        expToLvl = lvl * (10 + lvl) - surplusExp;
        if(amt > expToLvl)
        {
            surplusExp = amt - expToLvl;
            //exp += surplusExp;
            LvlUp();
            expToLvl = lvl * (10 + lvl) - surplusExp;
            while (expToLvl < 0)
            {
                LvlUp();
                surplusExp -= lvl * (10 + lvl);
                expToLvl = lvl * (10 + lvl) - surplusExp;
            }
        }
    }

    public void RestoreMana(int amt)
    {
        currentMana += amt;
        if(currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }

    public void RestoreHp(int amt)
    {
        currentHp += amt;
        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    public Player(string playerName)
    {
        name = playerName;
    }

    public void AddStr(int amt)
    {
        str += amt;
        dmg += lvl * amt;
        statPts -= amt;
    }

    public void AddVit(int amt)
    {
        vit += amt;
        def += lvl * amt;
        //maxHp += lvl * 10 * amt;
        currentHp = maxHp;
        statPts -= amt;
    }

    public void AddWis(int amt)
    {
        wis += amt;
        mdmg += lvl * amt;
        mdef += lvl * amt;
        currentMana = maxMana;
        statPts -= amt;
    }

}
