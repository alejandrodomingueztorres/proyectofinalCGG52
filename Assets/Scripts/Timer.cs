using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/// <summary>
/// Temporizador regresivo que se muestra en pantalla.
/// Al llegar a cero, reinicia el temporizador y llama al método de respawn en el GameManager.
/// </summary>
public class Timer : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Referencia al componente de texto que mostrará el tiempo restante.
    /// </summary>
    [SerializeField] private TMP_Text timerText;

    /// <summary>
    /// Tiempo total en segundos que tendrá el temporizador.
    /// </summary>
    [SerializeField, Tooltip("Tiempo en segundos")] private float timerTime;
    
    private int minutes, seconds, cents;
    private float startTime; // Tiempo original para reiniciar
    #endregion

    #region Métodos Unity
    /// <summary>
    /// Inicializa el temporizador guardando el valor original para futuros reinicios.
    /// </summary>
    void Start()
    {
        startTime = timerTime; // Guarda el tiempo inicial
    }

    /// <summary>
    /// Se llama una vez por frame. Actualiza el temporizador, lo muestra en pantalla y
    /// ejecuta el respawn del jugador si el tiempo llega a cero.
    /// </summary>
    void Update()
    {
        timerTime -= Time.deltaTime;

        if (timerTime < 0) timerTime = 0;

        minutes = (int)(timerTime / 60f);
        seconds = (int)(timerTime - minutes * 60f);
        cents = (int)((timerTime - (int)timerTime) * 100f);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, cents);

        if (timerTime ==0)
        {
            GameManager.instance.Respawn();
            timerTime = startTime; // Reinicia el temporizador
        }
    }

    internal float GetRemainingTime()
    {
       return timerTime;
    }
    #endregion

}
