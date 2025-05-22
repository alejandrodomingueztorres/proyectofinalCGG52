using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Representa un punto de entrada a un nivel en el mapa de selección.
/// Verifica si el nivel está desbloqueado, muestra información en la UI,
/// y permite cargar la escena correspondiente.
/// </summary>
public class LevelEntry : MonoBehaviour
{
    /// <summary>
    /// Nombre de la escena del nivel a cargar, Nombre de la clave en PlayerPrefs usada para verificar si el nivel está desbloqueado, Nombre mostrado en la interfaz para este punto de nivel.
    /// </summary>
    public string levelName, levelToCheck, displayName;

    /// <summary>
    /// Indica si el jugador puede cargar este nivel (por estar dentro del área).
    /// </summary>
    public bool canLoadLevel;

    /// <summary>
    /// Elemento visual mostrado si el nivel está desbloqueado.
    /// </summary>
    public GameObject mapPointActive;

    /// <summary>
    /// Elemento visual mostrado si el nivel está bloqueado.
    /// </summary>
    public GameObject mapPointInactive;

    private bool levelUnlocked;
    private bool levelLoading;

    /// <summary>
    /// Verifica el estado del nivel (bloqueado/desbloqueado) al iniciar,
    /// actualiza la interfaz y posiciona al jugador si es el nivel actual.
    /// </summary>
    private void Start()
    {
        if(PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1 || levelToCheck == "") 
        {
            mapPointActive.SetActive(true);
            mapPointInactive.SetActive(false);
            levelUnlocked = true;
        }else
        {
            mapPointActive.SetActive(false);
            mapPointInactive.SetActive(true);
            levelUnlocked = false;
        }

        if(PlayerPrefs.GetString("CurrentLevel")== levelName)
        {
            PlayerController.instance.transform.position = transform.position;
            ResetPlayer.instance.respawnPosition= transform.position; 
        }
    }

    /// <summary>
    /// Escucha la entrada del jugador para activar la carga del nivel si está en rango.
    /// </summary>
    private void Update()
    {
        if(Input.GetButtonDown("Jump") && canLoadLevel && levelUnlocked && !levelLoading)
        {
            StartCoroutine("LevelLoadWaiter");
            levelLoading = true;

            
        }
    }

    /// <summary>
    /// Detecta la entrada del jugador al área del nivel, permite cargarlo y muestra información.
    /// </summary>
    /// <param name="other">Collider del objeto que entra en contacto.</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canLoadLevel = true;

            LSUIManager.instance.lNamePanel.SetActive(true);
            LSUIManager.instance.lNameText.text = displayName;

            if(PlayerPrefs.HasKey(levelName + "_coins"))
            {
                LSUIManager.instance.coinsText.text = PlayerPrefs.GetInt(levelName + "_coins").ToString();
            }else
            {
                LSUIManager.instance.coinsText.text = "???";
            }
        }
    }

    /// <summary>
    /// Detecta la salida del jugador del área del nivel y oculta la interfaz correspondiente.
    /// </summary>
    /// <param name="other">Collider del objeto que sale del área.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canLoadLevel = false;
        }

        LSUIManager.instance.lNamePanel.SetActive(false);
    }

    /// <summary>
    /// Corrutina que espera antes de cargar el nivel seleccionado. 
    /// Detiene el movimiento del jugador y activa una animación de fundido.
    /// </summary>
    public IEnumerator LevelLoadWaiter()
    {
        PlayerController.instance.stopMove = true;
        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(levelName);
        PlayerPrefs.SetString("CurrentLevel", levelName);
    }
}
