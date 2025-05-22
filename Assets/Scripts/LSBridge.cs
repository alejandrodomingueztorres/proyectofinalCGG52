using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla la visibilidad de un GameObject en función del estado de desbloqueo de un nivel.
/// </summary>
/// <remarks>
/// Este script se utiliza para puentes, portales u otros elementos que solo deben aparecer
/// cuando un nivel específico ha sido desbloqueado.
/// La información de desbloqueo se almacena en <c>PlayerPrefs</c> con la clave formada por
/// el nombre del nivel seguido de "unlocked".
/// </remarks>
public class LSBridge : MonoBehaviour
{
    /// <summary>
    /// Nombre del nivel cuya condición de desbloqueo determina si este GameObject está activo.
    /// </summary>
    [Tooltip("Nombre del nivel que debe estar desbloqueado para activar este GameObject.")]
    public string levelToUnlock;

    /// <summary>
    /// Verifica si el nivel está desbloqueado al iniciar el juego.
    /// </summary>
    /// <remarks>
    /// Si el valor de <c>PlayerPrefs</c> para la clave <c>levelToUnlock + "unlocked"</c> es 0,
    /// el GameObject se desactiva.
    /// </remarks>
    void Start()
    {
        if(PlayerPrefs.GetInt(levelToUnlock + "unlocked") == 0)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Método llamado en cada frame (actualmente sin implementación).
    /// </summary>
    void Update()
    {
        
    }
}
