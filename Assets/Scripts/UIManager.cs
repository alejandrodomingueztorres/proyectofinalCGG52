using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Controlador central de la interfaz de usuario del juego.
/// Maneja transiciones visuales, elementos de HUD, control de volumen, menú de pausa y navegación entre escenas.
/// </summary>
public class UIManager : MonoBehaviour
{
    #region Singleton
    /// <summary>
    /// Instancia única del UIManager para acceso global.
    /// </summary>
    public static UIManager instance;

    #region Transiciones
    /// <summary>
    /// Imagen usada como pantalla negra para transiciones.
    /// </summary>
    public Image blackscreen;

    /// <summary>
    /// Velocidad de desvanecimiento de la pantalla negra.
    /// </summary>
    public float fadeSpeed;

    /// <summary>
    /// Indica si se debe hacer una transición hacia negro, Indica si se debe hacer una transición desde negro.
    /// </summary>
    public bool fadeToBlack, fadeFromBlack;
    #endregion

    #region HUD
    /// <summary>
    /// Texto que muestra el valor numérico de la salud del jugador.
    /// </summary>
    public Text healthTex;

    /// <summary>
    /// Imagen que representa la barra de salud del jugador.
    /// </summary>
    public Image healthImage;

    /// <summary>
    /// Texto que muestra la cantidad de monedas recogidas.
    /// </summary>
    public Text coinText;
    #endregion

    #region Menú de Pausa
    /// <summary>
    /// Objeto que representa la pantalla de pausa.
    /// </summary>
    public GameObject pauseScreen;

    /// <summary>
    /// Control deslizante para el volumen de la música, Control deslizante para el volumen de los efectos de sonido.
    /// </summary>
    public Slider musicVolSlider, sfxVolSlider;
    #endregion

    #region Escenas
    /// <summary>
    /// Nombre de la escena del menú principal, Nombre de la escena de selección de niveles.
    /// </summary>
    public string mainMenu, levelSelect;
    #endregion

    /// <summary>
    /// Inicializa la instancia del singleton UIManager.
    /// </summary>
    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region Métodos de Unity
    /// <summary>
    /// Método llamado al inicio del ciclo de vida del componente.
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Método llamado una vez por frame. Controla las transiciones de desvanecimiento de pantalla.
    /// </summary>
    void Update()
    {
        if (fadeToBlack)
        {
            blackscreen.color = new Color(blackscreen.color.r, blackscreen.color.g, blackscreen.color.b,
                Mathf.MoveTowards(blackscreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (blackscreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        if (fadeFromBlack)
        {
            blackscreen.color = new Color(blackscreen.color.r, blackscreen.color.g, blackscreen.color.b,
                Mathf.MoveTowards(blackscreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (blackscreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }
    #endregion

    #region Menú de Pausa
    /// <summary>
    /// Reinicia el estado del juego (pausa/reanuda).
    /// </summary>
    public void Reset()
    {
        GameManager.instance.PauseUnPase();
    }

    /// <summary>
    /// Reanuda el juego desde el menú de pausa.
    /// </summary>
    public void Resume()
    {
        GameManager.instance.PauseUnPase();
    }

    /// <summary>
    /// Abre el menú de opciones (actualmente sin implementación).
    /// </summary>
    public void OpenOptions()
    {
       
    }

    /// <summary>
    /// Cierra el menú de opciones (actualmente sin implementación).
    /// </summary>
    public void CloseOptions() 
    {
        
    }
    #endregion

    #region Navegación entre escenas
    /// <summary>
    /// Carga la escena de selección de niveles y reanuda el tiempo del juego.
    /// </summary>
    public void LevelSelect()
    {
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Carga la escena del menú principal y reanuda el tiempo del juego.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
    #endregion

    #region Volumen
    /// <summary>
    /// Ajusta el volumen de la música llamando al <c>AudioManager</c>.
    /// </summary>
    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }

    /// <summary>
    /// Ajusta el volumen de los efectos de sonido llamando al <c>AudioManager</c>.
    /// </summary>
    public void SetSFXLevel()
    {
        AudioManager.instance.SetSFXLevel();
    }
    #endregion
}

