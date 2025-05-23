using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Administra la salud del jugador, incluyendo el daño, curación, invencibilidad temporal,
/// y actualización de la interfaz gráfica relacionada con la salud.
/// </summary>
public class HealthManager : MonoBehaviour
{
    #region Singleton
    /// <summary>
    /// Instancia única del <c>HealthManager</c> para acceso global.
    /// </summary>
    public static HealthManager instance;
    #endregion

    #region Variables Públicas
    /// <summary>
    /// Salud actual del jugador, Salud máxima del jugador.
    /// </summary>
    public int currentHealth, maxHealth;

    /// <summary>
    /// Duración en segundos durante la cual el jugador es invencible después de recibir daño.
    /// </summary>
    public float invincibleLength = 2f;

    /// <summary>
    /// Contador interno de invencibilidad.
    /// </summary>
    private float invincCounter;

    /// <summary>
    /// Conjunto de sprites que representan la barra de salud en diferentes niveles.
    /// </summary>
    public Sprite[] healthBarImages;
    #endregion

    #region Métodos Unity
    /// <summary>
    /// Inicializa la instancia del singleton <c>HealthManager</c>.
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Método llamado al inicio del ciclo de vida del componente.
    /// Restablece la salud del jugador.
    /// </summary>
    void Start()
    {
        ResetHealth();
    }

    /// <summary>
    /// Método llamado una vez por frame. Controla la invencibilidad temporal
    /// y parpadeo visual de las piezas del jugador durante ese estado.
    /// </summary>
    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;


            for(int i = 0; i < PlayerController.instance.playerPieces.Length; i++)
            {
                if(Mathf.Floor(invincCounter*5f) % 2 == 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerController.instance.playerPieces[i].SetActive(false);
                }

                if (invincCounter <= 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
            }
        }
    }
    #endregion

    #region Control de Daño y Curación
    /// <summary>
    /// Aplica daño al jugador si no está en estado de invencibilidad.
    /// Si la salud llega a cero, se activa la reaparición desde el GameManager.
    /// </summary>
    public void Hurt()
    {
        if (invincCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.instance.Respawn();
            }
            else
            {
                PlayerController.instance.Knocback();
                invincCounter = invincibleLength;
            }
        }
        UpdateUI();

    }

    /// <summary>
    /// Restablece la salud del jugador a su valor máximo y actualiza la interfaz.
    /// </summary>
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UIManager.instance.healthImage.enabled = true;
        UpdateUI();
    }

    /// <summary>
    /// Aumenta la salud del jugador en una cantidad dada, sin exceder el valor máximo.
    /// </summary>
    /// <param name="amountToHealth">Cantidad de salud a agregar.</param>
    public void AddHealth(int amountToHealth)
    {
        currentHealth += amountToHealth;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateUI();
    }

    /// <summary>
    /// Actualiza la interfaz gráfica del jugador para reflejar su salud actual,
    /// incluyendo el sprite de la barra de vida correspondiente.
    /// </summary>
    public void UpdateUI()
    {
        UIManager.instance.healthTex.text = currentHealth.ToString();

        switch (currentHealth)
        {
            case 5:
                UIManager.instance.healthImage.sprite = healthBarImages[4];
                break;
            case 4:
                UIManager.instance.healthImage.sprite = healthBarImages[3];
                break;
            case 3:
                UIManager.instance.healthImage.sprite = healthBarImages[2];
                break;
            case 2:
                UIManager.instance.healthImage.sprite = healthBarImages[1];
                break;
            case 1:
                UIManager.instance.healthImage.sprite = healthBarImages[0];
                break;
            case 0:
                UIManager.instance.healthImage.enabled = false;
                break;
        }
    }

    /// <summary>
    /// Elimina toda la salud del jugador y actualiza la interfaz.
    /// Se puede usar en situaciones como muerte inmediata.
    /// </summary>
    public void PlayerKilled()
    {
        currentHealth = 0;
        UpdateUI(); 
    }
    #endregion
}
