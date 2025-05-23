using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla la salud de un enemigo, incluyendo el daño recibido y el comportamiento al morir.
/// Puede reproducir efectos, sonidos y generar ítems al morir.
/// </summary>
public class EnemyHealthmanager : MonoBehaviour
{
    #region Variables Públicas
    /// <summary>
    /// Salud máxima del enemigo.
    /// </summary>
    public int maxHealth = 1;

    /// <summary>
    /// Salud actual del enemigo.
    /// </summary>
    private int currentHealth;

    /// <summary>
    /// ID del efecto de sonido que se reproduce al morir.
    /// </summary>
    public int deathSound;

    /// <summary>
    /// Prefab del efecto visual que se instancia al morir, Prefab del objeto que el enemigo suelta al morir.
    /// </summary>
    public GameObject deathEffect, itemDrop;
    #endregion

    #region Métodos Unity
    /// <summary>
    /// Inicializa la salud actual del enemigo al valor máximo.
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;
    }
    #endregion

    #region Funciones Públicas
    /// <summary>
    /// Aplica daño al enemigo. Si su salud llega a cero o menos,
    /// reproduce sonido, destruye el enemigo, instancia efectos y objetos.
    /// También activa el rebote del jugador.
    /// </summary>
    public void TakeDamage()
    {
        currentHealth--;
        if(currentHealth <=0)
        {
            AudioManager.instance.PlaySFX(deathSound);
            Destroy(gameObject);

            Instantiate(deathEffect, transform.position, transform.rotation);
            Instantiate(itemDrop, transform.position, transform.rotation);
        }
        PlayerController.instance.Bounce();
    }
    #endregion
}
