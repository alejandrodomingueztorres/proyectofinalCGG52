using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representa una moneda recogible en el juego.
/// Al colisionar con el jugador, agrega monedas al total del GameManager,
/// reproduce un efecto visual y un sonido, y destruye el objeto.
/// </summary>
public class CoinPickup : MonoBehaviour
{
    /// <summary>
    /// Valor numérico de la moneda que se añadirá al contador del jugador.
    /// </summary>
    public int value;

    /// <summary>
    /// Prefab del efecto visual que se instanciará al recoger la moneda.
    /// </summary>
    public GameObject coinEffect;

    /// <summary>
    /// Índice del efecto de sonido que se reproducirá al recoger la moneda.
    /// </summary>
    public int soundToPlay;

    /// <summary>
    /// Inicializa el objeto. Este método se llama antes de la primera actualización del frame.
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Se llama una vez por frame.
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// Método llamado automáticamente por Unity cuando otro collider entra en el trigger de esta moneda.
    /// Si el objeto que entra es el jugador, se agrega el valor de la moneda, se reproduce un efecto
    /// visual y un sonido, y la moneda se destruye.
    /// </summary>
    /// <param name="other">El collider del objeto que entra en contacto con la moneda.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.AddCoins(value);
            Destroy(gameObject);
            Instantiate(coinEffect, transform.position, transform.rotation);
            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }
}
