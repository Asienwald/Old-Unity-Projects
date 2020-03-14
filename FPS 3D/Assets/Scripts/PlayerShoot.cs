using UnityEngine;
using Mirror;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";

    
/*    [SerializeField]
    private GameObject weaponGFX;*/
 
    
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    private PlayerWeapon currentWeapon;
    private WeaponManager weaponManager;

    private void Start()
    {
        if(cam == null)
        {
            Debug.Log("No Camera referenced.");
            this.enabled = false;
        }

        /*        weaponGFX.layer = LayerMask.NameToLayer(weaponLayerName);*/

        weaponManager = GetComponent<WeaponManager>();
    }


    private void Update()
    {
        currentWeapon = weaponManager.GetCurrentWeapon();

        if(currentWeapon.fireRate <= 0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f/currentWeapon.fireRate);
                // InvokeRepeating("Shoot", 0f, currentWeapon.fireRate);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }

    }

    // called on server when any player shoots
    [Command]
    void CmdOnShoot()
    {
        RpcDoShootEffects();
    }

    // called on all clients when doing shoot effect
    [ClientRpc]
    void RpcDoShootEffects()
    {
        Debug.Log("Doing shoot effects!");
        Debug.Log(weaponManager.GetCurrentGraphics().muzzleFlash);

        ParticleSystem _ps = weaponManager.GetCurrentGraphics().muzzleFlash;
        _ps.Clear();
        if (!_ps.isPlaying)
        {
            _ps.Play();
        }
    }

   
    [Client]    // methods called only on clients
    void Shoot()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        // shooting, call onshoot method on server
        CmdOnShoot();

        Debug.Log("Shoot!");
        RaycastHit _hit; // store info on item hit
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, currentWeapon.range, mask))
        {
            // hit sth
            if(_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name, currentWeapon.damage);
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
