using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el comportamiento y las fases de un jefe final en el juego.
/// Gestiona las transiciones entre fases, el sistema de daño, efectos de audio y la victoria.
/// </summary>
public class BossController : MonoBehaviour
{
    #region Singleton
    /// <summary>
    /// Instancia singleton para acceso global al controlador del jefe
    /// </summary>
    public static BossController instance;
    #endregion

    #region Referencias de Componentes
    /// <summary>
    /// Componente Animator que controla las animaciones del jefe
    /// </summary>
    public Animator animator;
    /// <summary>
    /// Zona de victoria que se activa cuando el jefe es derrotado
    /// </summary>
    public GameObject victoryZone;
    #endregion

    #region Configuración de Tiempo
    /// <summary>
    /// Tiempo de espera en segundos antes de mostrar la zona de salida/victoria
    /// </summary>
    public float waitToShowExit;
    #endregion

    #region IDs de Audio
    /// <summary>
    /// ID del audio de música de fondo durante la pelea con el jefe
    /// </summary>
    /// <summary>
    /// ID del efecto de sonido cuando el jefe muere
    /// </summary>
    /// <summary>
    /// ID del grito de muerte del jefe
    /// </summary>
    /// <summary>
    /// ID del efecto de sonido cuando el jefe recibe daño
    /// </summary>
    public int bossMusic, bossDeath, bossDeathShout, bossHit;
    #endregion

    #region Enumeración de Fases
    /// <summary>
    /// Define las diferentes fases de la pelea con el jefe
    /// </summary>
    public enum BossPhase
    {
        /// <summary>Fase de introducción/inicio</summary>
        intro,
        /// <summary>Primera fase de combate</summary>
        phase1,
        /// <summary>Segunda fase de combate</summary>
        phase2,
        /// <summary>Tercera fase de combate</summary>
        phase3,
        /// <summary>Fase final - jefe derrotado</summary>
        end,
    };

    /// <summary>
    /// Fase actual de la pelea con el jefe
    /// </summary>
    public BossPhase currentPhase = BossPhase.intro;
    #endregion

    #region Métodos de Unity
    /// <summary>
    /// Inicializa la instancia singleton al despertar el objeto
    /// </summary>
    private void Awake()
    {
        instance = this;
    }
    /// <summary>
    /// Inicialización al comenzar el juego
    /// </summary>
    void Start()
    {
        
    }
    /// <summary>
    /// Se ejecuta cuando el GameObject se activa
    /// Inicia la música del jefe
    /// </summary>
    public void OnEnable()
    {
        AudioManager.instance.PlayMusic(bossMusic);
    }
    /// <summary>
    /// Se ejecuta en cada frame
    /// Maneja la lógica de respawn del jugador y reseteo del jefe
    /// </summary>
    void Update()
    {
        // Si el jugador está reapareciendo, resetear el estado del jefe
        if (GameManager.instance.isRespawning)
        {
            currentPhase = BossPhase.intro;

            animator.SetBool("Phase1", false);
            animator.SetBool("Phase2", false);
            animator.SetBool("Phase3", false);

            AudioManager.instance.PlayMusic(AudioManager.instance.levelMusicToPlay);

            gameObject.SetActive(false);

            BossActivator.instance.gameObject.SetActive(true);
            BossActivator.instance.entrance.SetActive(true);

            GameManager.instance.isRespawning = false;
        }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Aplica daño al jefe y avanza a la siguiente fase
    /// Controla las transiciones de animación y efectos de audio
    /// </summary>
    public void DamageBoss()
    {
        // Reproducir sonido de impacto
        AudioManager.instance.PlaySFX(bossHit);
        // Avanzar a la siguiente fase
        currentPhase++;
        // Reproducir animación de daño si no es la fase final
        if (currentPhase != BossPhase.end)
        {
            animator.SetTrigger("Hurt");
        }

        #endregion
        #region Métodos Privados
        /// <summary>
        /// Maneja las transiciones de animación según la fase actual
        /// </summary>

        switch (currentPhase)
        {
            case BossPhase.phase1:
                animator.SetBool("Phase1", true);
                break;

            case BossPhase.phase2:
                animator.SetBool("Phase2", true);
                animator.SetBool("Phase1", false);
                break;

            case BossPhase.phase3:
                animator.SetBool("Phase3", true);
                animator.SetBool("Phase2", false);
                break;

            case BossPhase.end:
                animator.SetTrigger("End");
                StartCoroutine(EndBoss());
                break;
        }
    }
    #endregion

    #region Corrutinas
    /// <summary>
    /// Corrutina que maneja la secuencia de finalización del jefe
    /// Reproduce efectos de audio, espera un tiempo y activa la zona de victoria
    /// </summary>
    /// <returns>IEnumerator para la corrutina</returns>
    IEnumerator EndBoss()
    {
        // Reproducir efectos de sonido de muerte
        AudioManager.instance.PlaySFX(bossDeath);
        AudioManager.instance.PlaySFX(bossDeathShout);
        // Restaurar música del nivel
        AudioManager.instance.PlayMusic(AudioManager.instance.levelMusicToPlay);
        // Esperar antes de mostrar la salida
        yield return new WaitForSeconds(waitToShowExit);
        // Activar zona de victoria
        victoryZone.SetActive(true);
    }
    #endregion
}
