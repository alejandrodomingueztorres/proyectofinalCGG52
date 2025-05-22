using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gestiona la funcionalidad del menú principal, incluyendo iniciar un nuevo juego, continuar,
/// salir del juego y reiniciar el progreso.
/// </summary>
/// <remarks>
/// Este script debe asignarse a un GameObject en la escena del menú principal.
/// Utiliza PlayerPrefs para almacenar y recuperar el estado del juego.
/// </remarks>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Nombre de la primera escena que se carga al iniciar un nuevo juego.
    /// </summary>
    [Tooltip("Nombre de la escena que se cargará al iniciar un nuevo juego.")]
    public string firstLevel;

    /// <summary>
    /// Nombre de la escena del selector de niveles.
    /// </summary>
    [Tooltip("Nombre de la escena que se cargará al continuar un juego.")]
    public string levelSelect;

    /// <summary>
    /// Referencia al botón de continuar en el menú.
    /// </summary>
    [Tooltip("Botón de continuar que se activa si hay progreso guardado.")]
    public GameObject continueButton;

    /// <summary>
    /// Lista de nombres de niveles utilizados para reiniciar el progreso.
    /// </summary>
    [Tooltip("Lista de niveles usados para reiniciar su estado en PlayerPrefs.")]
    public string[] levelNames;

    /// <summary>
    /// Inicializa el menú verificando si hay progreso guardado.
    /// </summary>
    /// <remarks>
    /// Si existe la clave "Continue" en PlayerPrefs, se activa el botón de continuar.
    /// De lo contrario, se reinicia el progreso del jugador.
    /// </remarks>
    private void Start()
    {
        if (PlayerPrefs.HasKey("Continue"))
        {
            continueButton.SetActive(true);
        } else
        {
            ResetProgress();
        }
    }

    /// <summary>
    /// Inicia un nuevo juego cargando la escena especificada y reiniciando el progreso.
    /// </summary>
    public void NewGame()
    {
        SceneManager.LoadScene(firstLevel);

        PlayerPrefs.SetInt("Continue", 0);
        PlayerPrefs.SetString("CurrentLevel", firstLevel);

        ResetProgress();
    }

    /// <summary>
    /// Continúa el juego cargando la escena del selector de niveles.
    /// </summary>
    public void Continue()
    {
        SceneManager.LoadScene(levelSelect);
    }

    /// <summary>
    /// Cierra la aplicación.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Reinicia el progreso del jugador bloqueando todos los niveles.
    /// </summary>
    public void ResetProgress()
    {
        for(int i = 0; i < levelNames.Length; i++)
        {
            PlayerPrefs.SetInt(levelNames[i] + "_unlocked", 0);
        }
    }
}
