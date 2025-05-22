using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permite que un personaje se emparente dinámicamente con plataformas móviles
/// cuando está sobre ellas, para que herede el movimiento de dichas plataformas.
/// </summary>
public class CharacterParenter : MonoBehaviour
{
    /// <summary>
    /// Capa que contiene las plataformas móviles detectables.
    /// </summary>
    [SerializeField] private LayerMask platformLayer;

    /// <summary>
    /// Distancia máxima del raycast hacia abajo para detectar plataformas.
    /// </summary>
    [SerializeField] private float checkDistance = 0.1f;

    /// <summary>
    /// Indica si se deben mostrar mensajes de depuración en la consola.
    /// </summary>
    [SerializeField] private bool showDebug = true;

    private Transform originalParent;
    private CharacterController characterController;
    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;

    /// <summary>
    /// Inicializa referencias y verifica que el objeto tenga un CharacterController.
    /// </summary>
    void Start()
    {
        originalParent = transform.parent;
        characterController = GetComponent<CharacterController>();

        if (characterController == null)
        {
            Debug.LogError("CharacterParenter requiere un CharacterController en el mismo objeto", this);
            enabled = false;
        }
    }

    /// <summary>
    /// Se ejecuta cada frame. Realiza detección de plataformas debajo del jugador y
    /// establece la relación de parentesco según corresponda.
    /// </summary>
    void Update()
    {
        if (!characterController.isGrounded)
        {
            // El jugador no está en el suelo, liberar de cualquier plataforma
            if (transform.parent != originalParent)
            {
                transform.SetParent(originalParent);
                currentPlatform = null;
                if (showDebug) Debug.Log("El jugador no está en el suelo, liberando parentesco");
            }
            return;
        }

        // Lanzar raycast hacia abajo para detectar plataformas
        RaycastHit hit;
        Vector3 rayStart = transform.position + new Vector3(0, 0.1f, 0);
        if (Physics.Raycast(rayStart, Vector3.down, out hit, checkDistance + 0.1f, platformLayer))
        {
            // Verificar si la plataforma tiene el componente PlataformaMovil
            PlattformMovil plataforma = hit.collider.GetComponent<PlattformMovil>();
            if (plataforma != null)
            {
                // Si no estamos ya emparentados, hacernos hijo
                if (transform.parent != hit.transform)
                {
                    transform.SetParent(hit.transform);
                    currentPlatform = hit.transform;
                    lastPlatformPosition = currentPlatform.position;
                    if (showDebug) Debug.Log("Jugador sobre plataforma móvil, emparentando");
                }
            }
            else if (transform.parent != originalParent)
            {
                // Si estamos sobre otra superficie que no es una plataforma móvil
                transform.SetParent(originalParent);
                currentPlatform = null;
                if (showDebug) Debug.Log("Jugador fuera de plataforma móvil, restaurando parentesco original");
            }
        }
        else if (transform.parent != originalParent)
        {
            // No detectamos ninguna superficie debajo (podría estar cayendo)
            transform.SetParent(originalParent);
            currentPlatform = null;
            if (showDebug) Debug.Log("No se detecta superficie, restaurando parentesco");
        }
    }

    /// <summary>
    /// Dibuja una línea en el editor de Unity para visualizar el raycast que se usa para detectar plataformas.
    /// </summary>
    void OnDrawGizmos()
    {
        // Visualizar el raycast de detección
        Gizmos.color = Color.red;
        Vector3 rayStart = transform.position + new Vector3(0, 0.1f, 0);
        Gizmos.DrawLine(rayStart, rayStart + Vector3.down * (checkDistance + 0.1f));
    }
}
