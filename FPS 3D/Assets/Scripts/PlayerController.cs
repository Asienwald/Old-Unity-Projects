using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    // make it show up in inspector even though its private
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    [SerializeField]
    private float thrusterForce = 3000f;


    [Header("Spring Settings")]
    [SerializeField]
    private JointProjectionMode jointMode = JointProjectionMode.PositionAndRotation;
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;

    // component caching
    private PlayerMotor motor;
    private ConfigurableJoint joint;
    private Animator animator;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        animator = GetComponent<Animator>();

        SetJointSettings(jointSpring);
    }

    private void Update()
    {
        // calculate movement velocity as 3d vector
        // these axis are setup in unity project settings
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveHorizontal = transform.right * moveX;
        Vector3 moveVertical = transform.forward * moveZ;

        // normalised mean total combined max length should be 1
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        // animate movement
        animator.SetFloat("ForwardVelocity", moveZ);

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

        float camRotation = rotX * lookSensitivity;

        // apply rotation
        motor.RotateCamera(camRotation);

        // calc thruster force based on input
        Vector3 _thrusterForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        }
        else
        {
            SetJointSettings(jointSpring);
        }

        // apply thruster force
        motor.ApplyThruster(_thrusterForce);
    }

    // change config joint settings when start
    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive { 
            positionSpring = _jointSpring, 
            maximumForce = jointMaxForce 
        };
    }
}
