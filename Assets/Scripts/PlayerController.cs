using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlador del jugador principal. Maneja el movimiento, rotación, animaciones, rebote y efectos de retroceso (knockback).
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Variables Públicas
    /// <summary>
    /// Instancia única del controlador del jugador.
    /// </summary>
    public static PlayerController instance;

    /// <summary>
    /// Velocidad de movimiento del jugador.
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// Fuerza del salto del jugador.
    /// </summary>
    public float jumpForce;

    /// <summary>
    /// Escala de gravedad personalizada aplicada al jugador.
    /// </summary>
    public float gravityScale = 5f;

    /// <summary>
    /// Velocidad de rotación del jugador hacia la dirección del movimiento.
    /// </summary>
    public float rotateSpeed = 5f;

    private bool isGrounded;
    private Vector3 moveDirection;
    private bool wasJumping = false;

    /// <summary>
    /// Componente CharacterController del jugador.
    /// </summary>
    public CharacterController charController;

    /// <summary>
    /// Cámara principal que sigue al jugador.
    /// </summary>
    public Camera playerCamara;

    /// <summary>
    /// Modelo 3D del jugador usado para rotación y animaciones.
    /// </summary>
    public GameObject playerModel;

    /// <summary>
    /// Controlador de animaciones del jugador.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Indica si el jugador está en estado de retroceso (knockback).
    /// </summary>
    public bool isKnocking;

    /// <summary>
    /// Duración del retroceso.
    /// </summary>
    public float knockBackLength = .5f;
    private float knockBackCounter;

    /// <summary>
    /// Potencia del retroceso en X (horizontal) e Y (vertical).
    /// </summary>
    public Vector2 knockBackPower;

    /// <summary>
    /// Piezas del jugador que se pueden usar para efectos visuales (por ejemplo, desmembramiento).
    /// </summary>
    public GameObject[] playerPieces;

    /// <summary>
    /// Fuerza aplicada al jugador al rebotar.
    /// </summary>
    public float bounceForce = 8f;

    /// <summary>
    /// Indica si el movimiento del jugador está detenido.
    /// </summary>
    public bool stopMove;
    #endregion

    #region Métodos Unity
    /// <summary>
    /// Inicializa la instancia única.
    /// </summary>
    public void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Se ejecuta antes de la primera actualización del frame. Asigna la cámara principal si no está definida.
    /// </summary>
    void Start()
    {
        playerCamara = Camera.main;

    }

    /// <summary>
    /// Se llama una vez por frame. Maneja movimiento, rotación, saltos, retroceso y animaciones.
    /// </summary>
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
    #endregion

    #region Knockback y Rebote
    /// <summary>
    /// Aplica el retroceso (knockback) al jugador.
    /// </summary>
    public void Knocback()
    {
        isKnocking = true;
        knockBackCounter = knockBackLength;
        Debug.Log("Knoicoked Back");
        moveDirection.y = knockBackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }

    /// <summary>
    /// Aplica un rebote vertical al jugador.
    /// </summary>
    public void Bounce()
    {
        moveDirection.y = bounceForce;
        charController.Move(moveDirection * Time.deltaTime);
    }
    #endregion
    /*
    // Métodos alternativos de detección de suelo (no usados con CharacterController)
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    */
}
