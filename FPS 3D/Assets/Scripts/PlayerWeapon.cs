using UnityEngine;

// so can change these values in inspector
[System.Serializable]
public class PlayerWeapon
{
    public string name = "Glock";

    public int damage = 10;
    public float range = 100f;

    // 0 means is single fire
    public float fireRate = 0f;

    public GameObject graphics;

    
}
