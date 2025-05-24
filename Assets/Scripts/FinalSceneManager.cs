using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;


/// <summary>
/// Administrador de la escena final del juego. Maneja la visualización del resumen de partida
/// y permite al jugador guardar sus datos de rendimiento en un archivo JSON.
/// </summary>
public class FinalSceneManager : MonoBehaviour
{
    /// <summary>
    /// Campo de entrada de texto donde el jugador ingresa su nombre.
    /// </summary>
    public TMP_InputField nombreInput;
    /// <summary>
    /// Texto que muestra el resumen de la partida (tiempo y monedas recolectadas).
    /// </summary>
    public TMP_Text resumenTexto;

    /// <summary>
    /// Tiempo final de la partida obtenido desde PlayerPrefs.
    /// </summary>
    private float tiempoFinal;

    /// <summary>
    /// Cantidad de monedas recolectadas durante la partida obtenida desde PlayerPrefs.
    /// </summary>
    private int monedas;

    /// <summary>
    /// Inicialización de la escena final. Lee los datos de rendimiento guardados en PlayerPrefs
    /// y actualiza la interfaz con el resumen de la partida.
    /// </summary>
    void Start()
    {
        // Leer datos guardados en PlayerPrefs
        tiempoFinal = PlayerPrefs.GetFloat("TiempoFinal");
        monedas = PlayerPrefs.GetInt("MonedasFinal");

        resumenTexto.text = "Tiempo: " + tiempoFinal.ToString("F2") + "s\nMonedas: " + monedas;

        Debug.Log("TiempoFinal recibido: " + tiempoFinal);
        Debug.Log("MonedasFinal recibido: " + monedas);
    }

    /// <summary>
    /// Guarda los datos de la partida en un archivo JSON en el directorio persistente.
    /// Crea un objeto GameData con el nombre del jugador, tiempo final y monedas recolectadas,
    /// luego lo serializa a JSON y lo guarda en el archivo "gamedata.json".
    /// </summary>
    public void GuardarDatos()
    {
        GameData data = new GameData();
        data.playerName = nombreInput.text;
        data.finalTime = tiempoFinal;
        data.collectedCoins = monedas;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/gamedata.json", json);

        Debug.Log("Datos guardados en: " + Application.persistentDataPath + "/gamedata.json");
    }
}