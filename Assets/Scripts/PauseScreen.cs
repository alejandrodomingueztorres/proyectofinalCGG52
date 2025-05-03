using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public GameObject Pause, optionsScreen;
    public bool juegoPausado = false;
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

    }

    public void MainMenu()
    {

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
