using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destruye el objeto de juego asociado después de un tiempo determinado.
/// </summary>
/// <remarks>
/// Este script se puede adjuntar a cualquier GameObject que deba destruirse automáticamente
/// después de un período de tiempo especificado por la variable <see cref="lifeTime"/>.
/// El método <c>Destroy</c> se llama en cada frame dentro de <c>Update</c>, pero como 
/// <c>Destroy</c> acepta un retraso en segundos, el objeto solo será destruido una vez pasado ese tiempo.
/// </remarks>
public class DestroyOverTime : MonoBehaviour
{
    #region Variables de Configuración
    /// <summary>
    /// Tiempo de vida en segundos antes de que el objeto se destruya.
    /// </summary>
    [Tooltip("Tiempo en segundos antes de destruir este GameObject.")]
    public float lifeTime;
    #endregion


    #region Métodos de Unity
    /// <summary>
    /// Método llamado al iniciar el script.
    /// </summary>
    /// <remarks>
    /// Este método está definido pero no realiza ninguna acción en este script.
    /// </remarks>
    void Start()
    {
        
    }

    /// <summary>
    /// Método llamado una vez por frame.
    /// </summary>
    /// <remarks>
    /// Llama a <c>Destroy</c> para eliminar el GameObject después de <see cref="lifeTime"/> segundos.
    /// Aunque se llama cada frame, Unity internamente gestiona el temporizador sin generar destrucciones múltiples.
    /// </remarks>
    void Update()
    {
        Destroy(gameObject, lifeTime);
    }
    #endregion
}
