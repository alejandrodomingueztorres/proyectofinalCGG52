using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

/// <summary>
/// Controlador de audio del juego. Maneja la reproducción de música y efectos de sonido,
/// así como el ajuste de sus volúmenes a través del mezclador de audio.
/// </summary>
public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager instance;
    #endregion

    #region Configuración de Audio
    public int levelMusicToPlay;
    public AudioSource[] music;
    public AudioSource[] sfx;
    public AudioMixerGroup musicMixer, sfxMixer;
    #endregion

    #region Métodos de Unity
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        switch (sceneIndex)
        {
            case 0: PlayMusic(2); break; // MainMenu - Cheery Monday
            case 1: PlayMusic(3); break; // Test1 - Delightful D
            case 2: PlayMusic(8); break; // LevelSelect - Pixelland
            case 3: PlayMusic(7); break; // Test2 - Mellowtron
            case 4: PlayMusic(4); break; // Test3 - Getting it Done
            case 5: PlayMusic(0); break; // Boss1 - Great Boss
            case 6: PlayMusic(2); break; // FinalScene - Cheery Monday
            default: PlayMusic(2); break;
        }
    }

    void Update() { }
    #endregion

    #region Métodos de Reproducción
    public void PlayMusic(int musicToPlay)
    {
        foreach (AudioSource track in music)
        {
            track.Stop();
        }
        music[musicToPlay].Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Play();
    }
    #endregion

    #region Métodos de Control de Volumen
    public void SetMusicLevel()
    {
        musicMixer.audioMixer.SetFloat("Musicvol", UIManager.instance.musicVolSlider.value);
    }

    public void SetSFXLevel()
    {
        sfxMixer.audioMixer.SetFloat("SFXVol", UIManager.instance.sfxVolSlider.value);
    }
    #endregion
}