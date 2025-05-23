using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Activa al jefe final y cierra la entrada cuando el jugador entra en la zona de activación.
/// </summary>
/// <remarks>
/// Este script debe colocarse en un GameObject con un collider marcado como trigger.
/// Al detectar al jugador, activa el GameObject del jefe y desactiva la entrada para evitar la salida.
/// </remarks>
public class BossActivator : MonoBehaviour
{
    #region Singleton
    /// <summary>
    /// Instancia única de BossActivator para uso global.
    /// </summary>
    public static BossActivator instance;
    #endregion

    #region Variables Públicas
    /// <summary>
    /// Objeto de la entrada que se desactivará al iniciar el combate contra el jefe, Objeto del jefe que se activará al detectar al jugador.
    /// </summary>
    public GameObject entrance, theBoss;
    #endregion

    #region Métodos de Unity
    /// <summary>
    /// Asigna esta instancia como la única del tipo BossActivator.
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Detecta la entrada del jugador al área de activación del jefe.
    /// </summary>
    /// <param name="other">Collider que entró en el trigger.</param>
    /// <remarks>
    /// Si el collider pertenece al jugador, se activa al jefe, se desactiva la entrada
    /// y este GameObject también se desactiva para evitar múltiples activaciones.
    /// </remarks>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            entrance.SetActive(false);
            theBoss.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    #endregion
}
