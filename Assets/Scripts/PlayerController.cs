using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 5f;
    public float rotateSpeed = 5f;

    private Vector3 moveDirection;
    private bool wasJumping = false;

    public CharacterController charController;
    public Camera playerCamara;
    public GameObject playerModel;

    public Animator animator;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCamara = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection = moveDirection * moveSpeed;
        moveDirection.y = yStore;

        bool isCurrentlyJumping = !charController.isGrounded && moveDirection.y > 0;
        // Salto
        if (charController.isGrounded)
        {
            moveDirection.y = -0.3f;

            if (wasJumping)
            {
                wasJumping = false;
                animator.SetTrigger("Land");
            }

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
                animator.SetTrigger("TakeOff");
                wasJumping = true;
            }
        }
        else
        {
            if (!isCurrentlyJumping && wasJumping)
            {
                wasJumping = false;
                animator.SetTrigger("StartFalling");
            }
        }


        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
        charController.Move(moveDirection * Time.deltaTime);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, playerCamara.transform.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        animator.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        animator.SetBool("Grounded", charController.isGrounded);

        animator.SetFloat("VerticalSpeed", moveDirection.y);
        animator.SetBool("IsJumping", isCurrentlyJumping);
    }

}
