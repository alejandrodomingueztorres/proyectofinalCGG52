using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParenter : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float checkDistance = 0.1f;
    [SerializeField] private bool showDebug = true;

    private Transform originalParent;
    private CharacterController characterController;
    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;

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

    void Update()
    {
        if (!characterController.isGrounded)
        {
            // No estamos en el suelo, liberar de cualquier plataforma
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

    void OnDrawGizmos()
    {
        // Visualizar el raycast de detección
        Gizmos.color = Color.red;
        Vector3 rayStart = transform.position + new Vector3(0, 0.1f, 0);
        Gizmos.DrawLine(rayStart, rayStart + Vector3.down * (checkDistance + 0.1f));
    }
}
