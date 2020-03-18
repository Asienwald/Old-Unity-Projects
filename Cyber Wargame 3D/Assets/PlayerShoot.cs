using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private PlayerWeapon currentWeapon;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) { 
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.Log("Shoot!");

        animator.SetTrigger("Shooting");

        RaycastHit _hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, currentWeapon.fireRange, mask)) { 
            Debug.Log("Hit: " + _hit.collider.name);
        }
    }
}
