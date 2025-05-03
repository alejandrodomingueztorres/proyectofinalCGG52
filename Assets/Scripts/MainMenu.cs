using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;

    public string levelSelect;


    public void NewGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void Continue()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void QuirGame()
    {
        Application.Quit();
    }
}
