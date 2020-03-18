using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject rightArm;

    [Header("Movement Settings")]
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float camRotationX = 0f;
    private float currentCamRotX = 0f;

    [Header("Rotation Settings")]
    [SerializeField]
    private float camRotLimit = 85f;

    private Animator animator;
    private Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

    }


    private void FixedUpdate()
    {
        ExecuteMovementControls();
    }

    private void ExecuteMovementControls()
    {
        MovePlayer();
        RotatePlayer();
        RotateCamera();
    }

    private void MovePlayer()
    {
        // in project settings
        float _moveX = Input.GetAxisRaw("Horizontal");
        float _moveZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveX;
        Vector3 _moveVertical = transform.forward * _moveZ;

       /* Debug.Log("Horiz" + _moveHorizontal);
        Debug.Log("Vert" + _moveVertical);*/

        // normalised means combined max length of 1
        velocity = (_moveHorizontal + _moveVertical).normalized * speed;


        if (velocity != Vector3.zero)
        {
            animator.SetBool("Moving", true);
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        else animator.SetBool("Moving", false);
    }

    private void RotatePlayer()
    {
        // calc player rotation (horizontal)
        float _rotX = Input.GetAxisRaw("Mouse X");
        rotation = new Vector3(0f, _rotX, 0f) * lookSensitivity;

        // math stuff needed for rotating in 3D
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
    }

    private void RotateCamera()
    {
        // calc camera rotation (vertical)
        float _rotY = Input.GetAxisRaw("Mouse Y");
        camRotationX = _rotY * lookSensitivity;

        if (cam != null)
        {
            // clamp max rotation to maxrotlimit
            currentCamRotX -= camRotationX;
            currentCamRotX = Mathf.Clamp(currentCamRotX, -camRotLimit, camRotLimit);

            // apply rotation to cam transform
            cam.transform.localEulerAngles = new Vector3(currentCamRotX, 0f, 0f);
            rightArm.transform.localEulerAngles = new Vector3(currentCamRotX, 0f, 0f);
        }
    }
}
