﻿using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";

    public PlayerWeapon weapon;
    
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    private void Start()
    {
        if(cam == null)
        {
            Debug.Log("No Camera referenced.");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

   
    [Client]    // methods called only on clients
    void Shoot()
    {
        RaycastHit _hit; // store info on item hit
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
            // hit sth
            if(_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name, weapon.damage);
            }
        }
    }


    [Command]   // methods called only on server
    void CmdPlayerShot(string _playerID, int _damage)
    {
        Debug.Log(_playerID + " has been shot.");

        Player _player = GameManager.GetPlayer(_playerID);

        _player.RpcTakeDamage(_damage);
    }
}
