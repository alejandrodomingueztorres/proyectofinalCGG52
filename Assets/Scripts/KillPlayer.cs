using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este componente detecta si el jugador colisiona con el objeto al que está adjunto,
/// y en ese caso llama al método de reaparición del GameManager.
/// </summary>
public class KillPlayer : MonoBehaviour
{
    #region Detección de Jugador
    /// <summary>
    /// Se activa cuando otro collider entra en el trigger de este objeto.
    /// Si el objeto que entra tiene la etiqueta "Player", se llama al método <c>Respawn()</c> del GameManager.
    /// </summary>
    /// <param name="other">El collider que entra en el trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.Respawn();
        }
    }
    #endregion
}
