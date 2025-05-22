using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permite reubicar al jugador a una posición de reaparición cuando entra en un área determinada.
/// Puede utilizarse como un sistema de recuperación desde una caída u obstáculo.
/// </summary>
public class ResetPlayer : MonoBehaviour
{
    /// <summary>
    /// Instancia única del <c>ResetPlayer</c> para acceso global.
    /// </summary>
    public static ResetPlayer instance;

    /// <summary>
    /// Posición a la que se reubicará el jugador al activarse el reseteo.
    /// </summary>
    public Vector3 respawnPosition;

    /// <summary>
    /// Asigna la instancia estática del <c>ResetPlayer</c>.
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Se ejecuta cuando otro collider entra en el trigger de este objeto.
    /// Si el jugador lo activa, se lo desactiva temporalmente, se mueve a la posición de reaparición y se reactiva.
    /// </summary>
    /// <param name="other">El collider que entra en el trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController.instance.gameObject.SetActive(false);
            PlayerController.instance.transform.position = respawnPosition;
            PlayerController.instance.gameObject.SetActive(true);
        }
    }
}
