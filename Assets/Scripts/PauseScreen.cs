using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controla la lógica de la pantalla de pausa, incluyendo opciones, reanudación, cambio de escena
/// y ajuste de volumen.
/// </summary>
/// <remarks>
/// Este script debe estar asociado a un GameObject que tenga los elementos visuales del menú de pausa.
/// Se encarga de activar/desactivar el menú, pausar el juego y permitir navegar entre menús.
/// </remarks>
public class PauseScreen : MonoBehaviour
{
    #region Variables Públicas
    /// <summary>
    /// Panel principal del menú de pausa, Panel del submenú de opciones.
    /// </summary>
    public GameObject Pause, optionsScreen;

    /// <summary>
    /// Indica si el juego está actualmente en estado de pausa.
    /// </summary>
    public bool juegoPausado = false;

    /// <summary>
    /// Slider para ajustar el volumen de la música, Slider para ajustar el volumen de los efectos de sonido.
    /// </summary>
    public Slider musicVolSlider, sfxVolSlider;
    #endregion

    #region Métodos de Unity
    /// <summary>
    /// Método llamado al iniciar el script (actualmente no realiza acciones).
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Verifica si se presionó Escape para pausar o reanudar el juego.
    /// </summary>
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if (juegoPausado )
            {
                Resume();
            }
            else
            {
                Pausar();
            }
        }
    }
    #endregion

    #region Control de Pausa
    /// <summary>
    /// Llama al método de GameManager para alternar el estado de pausa.
    /// </summary>
    public void Reset()
    {
        GameManager.instance.PauseUnPase();
    }

    /// <summary>
    /// Reanuda el juego desactivando el menú de pausa.
    /// </summary>
    public void Resume()
    {
        GameManager.instance.PauseUnPase();
    }

    /// <summary>
    /// Pausa el juego activando el menú y deteniendo el tiempo.
    /// </summary>
    public void Pausar()
    {
        Pause.SetActive(true);
        Time.timeScale = 0;
        juegoPausado = true;
    }
    #endregion

    #region Navegación de Menús
    /// <summary>
    /// Muestra el panel de opciones dentro del menú de pausa.
    /// </summary>
    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
        
    }

    /// <summary>
    /// Oculta el panel de opciones.
    /// </summary>
    public void CloseOptions()
    {
        optionsScreen.SetActive(false);

    }

    /// <summary>
    /// Carga la escena del selector de niveles.
    /// </summary>
    public void LevelSelect()
    {
        SceneManager.LoadScene(UIManager.instance.levelSelect);
    }

    /// <summary>
    /// Carga la escena del menú principal.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(UIManager.instance.mainMenu);
    }
    #endregion

    #region Control de Audio
    /// <summary>
    /// Llama al AudioManager para establecer el volumen de la música.
    /// </summary>
    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }

    /// <summary>
    /// Llama al AudioManager para establecer el volumen de los efectos de sonido.
    /// </summary>
    public void SetSFXLevel()
    {
        AudioManager.instance.SetSFXLevel();
    }
    #endregion
}
