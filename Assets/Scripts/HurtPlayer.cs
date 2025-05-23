using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este componente detecta colisiones con el jugador.
/// Cuando el jugador entra en el trigger, se llama al método <c>Hurt()</c>
/// de la instancia de <c>HealthManager</c> para aplicar daño.
/// </summary>
public class HurtPlayer : MonoBehaviour
{
    #region Métodos de Unity
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
    #endregion

    #region Detección de Jugador
    /// <summary>
    /// Se activa cuando otro collider entra en el trigger de este objeto.
    /// Si el collider pertenece al jugador, se aplica daño llamando a <c>HealthManager.instance.Hurt()</c>.
    /// </summary>
    /// <param name="other">El collider que entra en el trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            HealthManager.instance.Hurt();
        }
    }
    #endregion
}
