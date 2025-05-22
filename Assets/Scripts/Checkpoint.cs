using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representa un punto de control (checkpoint) en el juego.
/// Al activarse, actualiza el punto de reaparición del jugador y gestiona la activación visual de los checkpoints.
/// </summary>
public class Checkpoint : MonoBehaviour
{
    /// <summary>
    /// GameObject visual que indica que el checkpoint está activo, GameObject visual que indica que el checkpoint está inactivo.
    /// </summary>
    public GameObject CpON, CpOFF;

    /// <summary>
    /// Método llamado al inicio del ciclo de vida del componente.
    /// Actualmente no realiza ninguna acción.
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Método llamado una vez por frame.
    /// Actualmente no realiza ninguna acción.
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// Se ejecuta cuando otro collider entra en el trigger del checkpoint.
    /// Si el jugador lo activa, se actualiza el punto de reaparición y se desactivan los demás checkpoints.
    /// </summary>
    /// <param name="other">El collider que entra en el trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            // Actualiza el punto de reaparición del jugador
            GameManager.instance.SetSpawnPoint(transform.position);

            // Desactiva la visualización de todos los checkpoints
            Checkpoint[] allCP = FindObjectsOfType<Checkpoint>();
            for (int i = 0; i < allCP.Length; i++)
            {
                allCP[i].CpOFF.SetActive(true);
                allCP[i].CpON.SetActive(false);
            }

            // Activa visualmente este checkpoint
            CpOFF.SetActive(false);
            CpON.SetActive(true);
        }
    }
}
