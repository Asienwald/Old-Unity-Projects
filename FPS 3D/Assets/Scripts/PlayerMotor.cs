using System.Collections;
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
    private float camRotationX = 0f;
    private float currentCamRotX = 0f;
    private Vector3 thrusterForce = Vector3.zero;

    [SerializeField]
    private float cameraRotationLimit = 85f;

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

    public void RotateCamera(float _camRot)
    {
        camRotationX = _camRot;
    }

    // get force vector for our thrusters
    public void ApplyThruster(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
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

        if(thrusterForce != Vector3.zero)
        {
            // add acceleration to rigid body, ignoring mass
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            // rb.AddForce(thrusterForce, ForceMode.Acceleration);
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
            //cam.transform.Rotate(-camRotation);

            // new rotation calc using float with max rot = 85
            // set rotation & clamp it
            currentCamRotX -= camRotationX;
            currentCamRotX = Mathf.Clamp(currentCamRotX, -cameraRotationLimit, cameraRotationLimit);

            // apply rotation to transform of camera
            cam.transform.localEulerAngles = new Vector3(currentCamRotX, 0f, 0f);
        }
    }
}
