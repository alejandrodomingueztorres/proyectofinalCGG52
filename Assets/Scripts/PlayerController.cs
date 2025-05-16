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
    private bool isGrounded;

    private Vector3 moveDirection;
    private bool wasJumping = false;

    public CharacterController charController;
    public Camera playerCamara;
    public GameObject playerModel;

    public Animator animator;


    public bool isKnocking;
    public float knockBackLength = .5f;
    private float knockBackCounter;
    public Vector2 knockBackPower;

    public GameObject[] playerPieces;

    public float bounceForce = 8f;

    public bool stopMove;

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
        if (!isKnocking && !stopMove)
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
            animator.SetFloat("VerticalSpeed", moveDirection.y);
            animator.SetBool("IsJumping", isCurrentlyJumping);
        }
        if (isKnocking)
        {
            knockBackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = (playerModel.transform.forward * knockBackPower.x);
            moveDirection.y = yStore;

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            charController.Move(moveDirection * Time.deltaTime);

            if (knockBackCounter <= 0)
            {
                isKnocking = false;
            }
        }

        if (stopMove)
        {
            moveDirection = Vector3.zero;
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            charController.Move(moveDirection);
        }

        animator.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        animator.SetBool("Grounded", charController.isGrounded);

    }

    public void Knocback()
    {
        isKnocking = true;
        knockBackCounter = knockBackLength;
        Debug.Log("Knoicoked Back");
        moveDirection.y = knockBackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }

    public void Bounce()
    {
        moveDirection.y = bounceForce;
        charController.Move(moveDirection * Time.deltaTime);
    }

    //void OnCollisionEnter(Collision collision)
    //{

    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //    }
    //}

    //void OnCollisionExit(Collision collision)
    //{

    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = false;
    //    }
    //}
}
