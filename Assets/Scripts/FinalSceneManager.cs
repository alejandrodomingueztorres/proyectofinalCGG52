using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class FinalSceneManager : MonoBehaviour
{
    public TMP_InputField nombreInput;
    public TMP_Text resumenTexto;

    private float tiempoFinal;
    private int monedas;

    void Start()
    {
        // Leer datos guardados en PlayerPrefs
        tiempoFinal = PlayerPrefs.GetFloat("TiempoFinal");
        monedas = PlayerPrefs.GetInt("MonedasFinal");

        resumenTexto.text = "Tiempo: " + tiempoFinal.ToString("F2") + "s\nMonedas: " + monedas;
    }

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