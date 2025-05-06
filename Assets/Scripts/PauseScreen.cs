using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public GameObject Pause, optionsScreen;
    public bool juegoPausado = false;
    public Slider musicVolSlider, sfxVolSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
    public void Reset()
    {
        GameManager.instance.PauseUnPase();
    }

    public void Resume()
    {
        GameManager.instance.PauseUnPase();
    }

    public void Pausar()
    {
        Pause.SetActive(true);
        Time.timeScale = 0;
        juegoPausado = true;
    }
    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
        
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);

    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(UIManager.instance.levelSelect);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(UIManager.instance.mainMenu);
    }
    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }
    public void SetSFXLevel()
    {
        AudioManager.instance.SetSFXLevel();
    }
}
