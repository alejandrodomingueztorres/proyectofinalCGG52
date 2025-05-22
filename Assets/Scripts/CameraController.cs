using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Controlador de cámara principal que maneja la referencia al componente <c>CinemachineBrain</c>.
/// Se implementa como un singleton para permitir el acceso global a la instancia de la cámara.
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Instancia única del controlador de cámara.
    /// Permite el acceso global al componente desde otras clases.
    /// </summary>
    public static CameraController instance;

    /// <summary>
    /// Referencia al componente <c>CinemachineBrain</c> que gestiona la lógica de la cámara virtual.
    /// </summary>
    public CinemachineBrain cmBrain;

    /// <summary>
    /// Se ejecuta antes del método <c>Start()</c>.
    /// Asigna la instancia estática de este controlador.
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Método llamado al inicio del ciclo de vida del componente.
    /// Actualmente no contiene lógica adicional.
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Método llamado una vez por frame.
    /// Actualmente no contiene lógica adicional.
    /// </summary>
    void Update()
    {
        
    }
}
