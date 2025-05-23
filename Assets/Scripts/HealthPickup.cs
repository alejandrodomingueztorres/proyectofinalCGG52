using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el comportamiento de un objeto recolectable que restaura salud al jugador.
/// </summary>
/// <remarks>
/// Puede configurarse para curar una cantidad específica de salud o restaurar toda la salud del jugador.
/// También reproduce un efecto visual y un sonido al ser recogido.
/// </remarks>
public class HealthPickup : MonoBehaviour
{
    #region Variables de Configuración
    /// <summary>
    /// Cantidad de salud que se restaura si <see cref="isFullHeal"/> es falso.
    /// </summary>
    public int healAmount;

    /// <summary>
    /// Indica si este objeto de salud restaura toda la salud del jugador.
    /// </summary>
    public bool isFullHeal;

    /// <summary>
    /// Prefab del efecto visual que se instancia al recoger el objeto.
    /// </summary>
    public GameObject healthEffect;

    /// <summary>
    /// Índice del efecto de sonido que se reproduce al recoger el objeto.
    /// </summary>
    public int soundToPlay;
    #endregion

    #region Detección de Recolección
    /// <summary>
    /// Detecta la colisión con el jugador y aplica la curación correspondiente.
    /// </summary>
    /// <param name="other">Collider del objeto que entra en contacto con este recolectable.</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {


            Destroy(gameObject);

            Instantiate(healthEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);

            if (isFullHeal)
            {
                HealthManager.instance.ResetHealth();
                AudioManager.instance.PlaySFX(soundToPlay);
            }
            else
            {
                HealthManager.instance.AddHealth(healAmount);
                
            }
        }
    }

    #endregion
}
