﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// will always need rigidbody component to function
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 camRotation = Vector3.zero;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 velo)
    {
        velocity = velo;
    }

    public void Rotate(Vector3 rot)
    {
        rotation = rot;
    }

    public void RotateCamera(Vector3 camRot)
    {
        camRotation = camRot;
    }

    // run every physics iteration
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation()
    {
        // some math shit that is required for rotating
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        // presence of a cam object is optional
        // option for player not being able to look up
        if (cam != null)
        {
            // negative to inverse mouse rotation (mouse go up, cam goes up)
            cam.transform.Rotate(-camRotation);
        }
    }
}
