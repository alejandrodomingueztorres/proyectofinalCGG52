using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public int levelMusicToPlay;

    public AudioSource[] music;
    public AudioSource[] sfx;

    public AudioMixerGroup musicMixer, sfxMixer;

    private void Awake()
    {
        instance = this;    
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayMusic(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(int musicToPlay)
    { 
        music[musicToPlay].Play();
    
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Play();
    }
    public void SetMusicLevel()
    {
        musicMixer.audioMixer.SetFloat("Musicvol", UIManager.instance.musicVolSlider.value);
    }
    public void SetSFXLevel()
    {
        sfxMixer.audioMixer.SetFloat("SFXVol", UIManager.instance.sfxVolSlider.value);
    }


}
