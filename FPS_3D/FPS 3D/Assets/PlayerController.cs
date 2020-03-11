using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    // make it show up in inspector even though its private
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        // calculate movement velocity as 3d vector
        // these axis are setup in unity project settings
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveX;
        Vector3 moveVertical = transform.forward * moveZ;

        // normalised mean total combined max length should be 1
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        // apply movement
        motor.Move(velocity);

        // calculate rotation as 3d vector
        // calc X only as we don't want our player to rotate vertically > will mess up our movement
        // rotate vert only affects camera
        float rotY = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, rotY, 0f) * lookSensitivity;

        // apply player rotation
        motor.Rotate(rotation);


        // calculate camera rotation as 3d vector
        float rotX = Input.GetAxisRaw("Mouse Y");

        Vector3 camRotation = new Vector3(rotX, 0f, 0f) * lookSensitivity;

        // apply rotation
        motor.RotateCamera(camRotation);
    }
}
