using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Componente que detecta cuando el jugador entra en contacto con un área de daño del jefe.
/// Debe ser colocado en un GameObject con un Collider configurado como Trigger.
/// Al detectar al jugador, comunica al BossController que debe recibir daño y avanzar de fase.
/// </summary>
/// <remarks>
/// Este script requiere:
/// - Un Collider con "Is Trigger" activado en el mismo GameObject
/// - Que el jugador tenga el tag "Player"
/// - Que exista una instancia de BossController en la escena
/// </remarks>
public class DamageBoss : MonoBehaviour
{
    #region Eventos de Trigger
    /// <summary>
    /// Se ejecuta cuando otro Collider entra en el área del trigger.
    /// Detecta si el objeto que entra es el jugador y, de ser así,
    /// activa el sistema de daño del jefe a través del BossController.
    /// </summary>
    /// <param name="other">El Collider que ha entrado en el trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entró al trigger es el jugador
        if (other.tag == "Player")
        {
            // Comunicar al BossController que debe recibir daño
            // Esto causará que el jefe avance a la siguiente fase
            BossController.instance.DamageBoss();
        }
    }
    #endregion
}
