using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase de datos que almacena la información de rendimiento del jugador al finalizar el juego.
/// Utilizada para serializar y guardar los datos de la partida en formato JSON.
/// </summary>
public class GameData : MonoBehaviour
{

    /// <summary>
    /// Nombre del jugador ingresado por el usuario.
    /// </summary>
    public string playerName;
    /// <summary>
    /// Tiempo final que tardó el jugador en completar el juego, en segundos.
    /// </summary>
    public float finalTime;
    /// <summary>
    /// Cantidad total de monedas recolectadas durante la partida.
    /// </summary>
    public int collectedCoins;
    /// <summary>
    /// Se llama antes de la primera actualización del frame. Actualmente no se utiliza.
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /// <summary>
    /// Se llama una vez por frame. Actualmente no se utiliza.
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        
    }
}
