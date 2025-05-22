using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controlador principal del juego. Maneja el respawn del jugador, la gestión de monedas,
/// la pausa, el fin del nivel y mantiene una instancia global.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Instancia única del GameManager accesible globalmente.
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// Posición donde el jugador reaparecerá después de morir.
    /// </summary>
    private Vector3 respawnPosition;

    /// <summary>
    /// Efecto visual que se reproduce al morir el jugador.
    /// </summary>
    [Tooltip("Prefab del efecto de muerte del jugador.")]
    public GameObject DeathEffect;

    /// <summary>
    /// Número actual de monedas recolectadas por el jugador.
    /// </summary>
    public int currentCoins;

    /// <summary>
    /// ID de la música que se reproduce al finalizar el nivel.
    /// </summary>
    public int levelEndMusic;

    /// <summary>
    /// Nombre de la escena a cargar al completar el nivel.
    /// </summary>
    public string levelToLoad;

    /// <summary>
    /// Indica si el jugador está en proceso de respawn.
    /// </summary>
    public bool isRespawning;

    /// <summary>
    /// Asigna esta instancia como la única existente.
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Inicializa el estado del juego, desactiva el cursor y establece el punto de respawn.
    /// </summary>
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        respawnPosition = PlayerController.instance.transform.position;

        AddCoins(0);
    }

    /// <summary>
    /// Escucha la tecla Escape para alternar entre pausa y juego.
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPase();
        }
    }

    /// <summary>
    /// Inicia el proceso de respawn del jugador.
    /// </summary>
    public void Respawn()
    {
        StartCoroutine(RespawnWaiter());
        HealthManager.instance.PlayerKilled();
    }

    /// <summary>
    /// Corrutina que maneja el efecto visual y la lógica de reaparecer tras morir.
    /// </summary>
    /// <returns>IEnumerator necesario para la corrutina.</returns>
    public IEnumerator RespawnWaiter()
    {
        PlayerController.instance.gameObject.SetActive(false);

        CameraController.instance.cmBrain.enabled = false;

        UIManager.instance.fadeToBlack = true;

        isRespawning = true;

        Instantiate(DeathEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);

        yield return new WaitForSeconds(2f);

        UIManager.instance.fadeFromBlack = true;

        PlayerController.instance.transform.position = respawnPosition;
        
        CameraController.instance.cmBrain.enabled = true;

        PlayerController.instance.gameObject.SetActive(true);

        HealthManager.instance.ResetHealth();

        isRespawning = false;
    }

    /// <summary>
    /// Establece una nueva posición de respawn para el jugador.
    /// </summary>
    /// <param name="newSpawnPoint">Posición en el mundo donde el jugador reaparecerá.</param>
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
        Debug.Log("Spawn Set");
    }

    /// <summary>
    /// Añade monedas al total del jugador y actualiza la UI.
    /// </summary>
    /// <param name="coinsToAdd">Cantidad de monedas a añadir.</param>
    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        UIManager.instance.coinText.text = "" + currentCoins;
    }

    /// <summary>
    /// Alterna entre estado de pausa y juego.
    /// </summary>
    public void PauseUnPase()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);  
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
    }

    /// <summary>
    /// Corrutina que maneja la lógica de finalización del nivel.
    /// </summary>
    /// <returns>IEnumerator para ejecutar acciones después de un retardo.</returns>
    public IEnumerator LevelEndWaiter()
    {
        AudioManager.instance.PlayMusic(levelEndMusic);
        PlayerController.instance.stopMove = true;

        yield return new WaitForSeconds(3f);


        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);

        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_coins"))
        {
            if(currentCoins > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_coins"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins", currentCoins);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins", currentCoins);
        }

        SceneManager.LoadScene(levelToLoad);
    }
}
