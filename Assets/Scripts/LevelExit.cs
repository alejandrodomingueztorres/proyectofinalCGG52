using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representa la salida de un nivel. Al ser activada por el jugador,
/// reproduce una animación y comienza la secuencia de finalización del nivel.
/// </summary>
public class LevelExit : MonoBehaviour
{
    /// <summary>
    /// Referencia al <c>Animator</c> responsable de reproducir la animación de salida.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Se ejecuta cuando otro collider entra en el trigger del objeto.
    /// Si el jugador lo activa, se reproduce una animación y se inicia la rutina de finalización de nivel.
    /// </summary>
    /// <param name="other">El collider que entra en el trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Activa la animación de salida
            animator.SetTrigger("Hit");

            // Inicia la rutina de finalización del nivel
            StartCoroutine(GameManager.instance.LevelEndWaiter());

        }
    }
}
