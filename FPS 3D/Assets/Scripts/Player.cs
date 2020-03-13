using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SyncVar]
    private bool _isDead = false;

    // cannot just do norm get set as it wont allow you to mark it as a syncvar
    public bool isDead
    {
        get { return _isDead; }
        // only player class or classes that derive from player class can change this
        protected set { _isDead = value; }
    }
    
    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]   //  everytime this value changes it will be pushed out to all clients
    private int currentHealth;

    [SerializeField]
    private Behaviour[] disabledOnDeath;
    private bool[] wasEnabled;

    // called when player setup is ready
    // must do disable after initial enable/disable done
    // called in playersetup script
    public void Setup()
    {
        wasEnabled = new bool[disabledOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disabledOnDeath[i].enabled;
        }
        
        SetDefaults();
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                RpcTakeDamage(99999999);
            }
        }
        else
        {
            return;
        }
    }



    [ClientRpc] // make sure this method is called on all clients so when take dmg will die
    public void RpcTakeDamage(int _amount)
    {
        if (isDead) return;

        currentHealth -= _amount;

        Debug.Log(transform.name + " now has " + currentHealth + " health.");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        // disable some components on player obj
        // so cannot move anymore etc.
        for (int i = 0; i < disabledOnDeath.Length; i++)
        {
            disabledOnDeath[i].enabled = false;
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null) _col.enabled = false;

        

        Debug.Log(transform.name + " is dead.");

        // call respawn method
        // use ienumerator as want wait certain amt of seconds

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

        SetDefaults();
        // singleton is instance of our networkmanager
        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;
    }

    public void SetDefaults()
    {
        isDead = false;

        currentHealth = maxHealth;

        for (int i = 0; i < disabledOnDeath.Length; i++)
        {
            disabledOnDeath[i].enabled = wasEnabled[i];
        }

        // monobehaviour derived from behaviours and can be enabled/disabled
        // colliders however derive from components which behaviour is derived from
        // components cannot be enabled/disabled
        // here is special case if collider exist on player obj
        Collider _col = GetComponent<Collider>();
        if (_col != null) _col.enabled = true;
    }
}
