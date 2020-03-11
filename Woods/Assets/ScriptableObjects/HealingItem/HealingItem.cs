using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Healing Item")]
public class HealingItem : Item {

    public string targetHeal;
    public int healAmt;

    public float coolDown = 5f;
}
