using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Zona de plataforma que maneja la vinculación del jugador con plataformas móviles.
/// Cuando el jugador entra en la zona, se convierte en hijo de la plataforma para moverse junto con ella.
/// Cuando el jugador sale de la zona, se desvincula de la plataforma.
/// </summary>
public class PlattformZone : MonoBehaviour
{
    /// <summary>
    /// Tag del jugador que será detectado por la zona de plataforma
    /// </summary>
    [SerializeField] private string playerTag = "Player";

    /// <summary>
    /// Se ejecuta cuando otro Collider entra en el área del trigger.
    /// Si el objeto que entra es el jugador, lo convierte en hijo de la plataforma
    /// para que se mueva junto con ella.
    /// </summary>
    /// <param name="other">El Collider que ha entrado en el trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            other.transform.SetParent(transform.parent); // El padre es la plataforma
        }
    }

    /// <summary>
    /// Se ejecuta cuando otro Collider sale del área del trigger.
    /// Si el objeto que sale es el jugador, lo desvincula de la plataforma
    /// restaurando su independencia de movimiento.
    /// </summary>
    /// <param name="other">El Collider que ha salido del trigger</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            other.transform.SetParent(null);
        }
    }
}
