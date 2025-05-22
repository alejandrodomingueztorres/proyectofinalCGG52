using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Controlador de audio del juego. Maneja la reproducción de música y efectos de sonido,
/// así como el ajuste de sus volúmenes a través del mezclador de audio.
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Instancia única del AudioManager para acceder globalmente.
    /// </summary>
    public static AudioManager instance;

    /// <summary>
    /// Índice de la pista de música del nivel que se debe reproducir.
    /// </summary>
    public int levelMusicToPlay;

    /// <summary>
    /// Arreglo de pistas de música disponibles.
    /// </summary>
    public AudioSource[] music;

    /// <summary>
    /// Arreglo de efectos de sonido disponibles.
    /// </summary>
    public AudioSource[] sfx;

    /// <summary>
    /// Mezclador de audio para la música, Mezclador de audio para los efectos de sonido.
    /// </summary>
    public AudioMixerGroup musicMixer, sfxMixer;

    /// <summary>
    /// Se ejecuta cuando se instancia el objeto. Inicializa la instancia única.
    /// </summary>
    private void Awake()
    {
        instance = this;    
    }

    /// <summary>
    /// Se llama antes de la primera actualización del frame.
    /// Reproduce la música de nivel predeterminada.
    /// </summary>
    void Start()
    {
        PlayMusic(2);
    }

    /// <summary>
    /// Se llama una vez por frame. Actualmente no se utiliza.
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// Reproduce una pista de música desde el arreglo de pistas.
    /// </summary>
    /// <param name="musicToPlay">Índice de la pista de música a reproducir.</param>
    public void PlayMusic(int musicToPlay)
    { 
        music[musicToPlay].Play();
    
    }

    /// <summary>
    /// Reproduce un efecto de sonido desde el arreglo de efectos.
    /// </summary>
    /// <param name="sfxToPlay">Índice del efecto de sonido a reproducir.</param>
    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Play();
    }

    /// <summary>
    /// Ajusta el volumen de la música utilizando el valor del control deslizante en UIManager.
    /// </summary>
    public void SetMusicLevel()
    {
        musicMixer.audioMixer.SetFloat("Musicvol", UIManager.instance.musicVolSlider.value);
    }

    /// <summary>
    /// Ajusta el volumen de los efectos de sonido utilizando el valor del control deslizante en UIManager.
    /// </summary>
    public void SetSFXLevel()
    {
        sfxMixer.audioMixer.SetFloat("SFXVol", UIManager.instance.sfxVolSlider.value);
    }
}
