using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image blackscreen;
    public float fadeSpeed;
    public bool fadeToBlack, fadeFromBlack;

    public Text healthTex;
    public Image healthImage;

    public Text coinText;

    public GameObject pauseScreen;

    public Slider musicVolSlider, sfxVolSlider;

    public string mainMenu, levelSelect;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

    public void Reset()
    {
        GameManager.instance.PauseUnPase();
    }

    public void Resume()
    {
        GameManager.instance.PauseUnPase();
    }

    public void OpenOptions()
    {
       
    }

    public void CloseOptions() 
    {
        
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
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

