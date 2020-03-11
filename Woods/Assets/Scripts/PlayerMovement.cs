using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public bool useJoyStick;

    public float moveSpeed;
    public Joystick joystick;

    private float moveX;
    private float moveY;

    private Animator anim;
    private float lastMoveX;
    private float lastMoveY;

    private void Start()
    {
        anim = GetComponent<Animator>();

        if (!useJoyStick)
        {
            joystick.gameObject.SetActive(false);   // hide the joystick
        }
    }

    private void ShowAnimation(int moveX, int moveY)
    {

    }

    private void Update()
    {
        if (useJoyStick)
        {
            Vector3 moveVector = (transform.right * joystick.Horizontal + transform.forward * joystick.Vertical).normalized;

            if (moveVector.x != 0f || moveVector.y != 0f)
            {
                lastMoveX = moveVector.x;
                lastMoveY = moveVector.z;
                anim.SetBool("Stationary", false);
            }
            else
            {
                anim.SetBool("Stationary", true);
                anim.SetFloat("LastMoveX", lastMoveX);
                anim.SetFloat("LastMoveY", lastMoveY);
            }

            moveX = moveVector.x;
            moveY = moveVector.z;
            Vector2 movePlayer = new Vector2(moveX, moveY);

            anim.SetFloat("MoveX", movePlayer.x);
            anim.SetFloat("MoveY", movePlayer.y);

            Debug.Log(moveVector);
            transform.Translate(movePlayer * moveSpeed * Time.deltaTime);
        }
        else
        {

            if (Input.anyKey)
            {
            
                if (Input.GetKey(KeyCode.W))
                {
                    moveY = 1;
                }else if (Input.GetKey(KeyCode.S))
                {
                    moveY = -1;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    moveX = -1;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    moveX = 1;
                }

                lastMoveX = moveX;
                lastMoveY = moveY;

                Vector2 movePlayer = new Vector2(moveX, moveY);
                anim.SetBool("Stationary", false);
                anim.SetFloat("MoveX", movePlayer.x);
                anim.SetFloat("MoveY", movePlayer.y);

                // transform.Translate(movePlayer * moveSpeed * Time.deltaTime);
                transform.Translate(Vector2.ClampMagnitude(movePlayer, 1) * moveSpeed * Time.deltaTime);
            }
            else
            {
                moveX = 0;
                moveY = 0;
                anim.SetBool("Stationary", true);
                anim.SetFloat("LastMoveX", lastMoveX);
                anim.SetFloat("LastMoveY", lastMoveY);
            }

            
        }
        
    }
}
