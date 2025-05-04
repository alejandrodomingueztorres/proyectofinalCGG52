using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEntry : MonoBehaviour
{

    public string levelName;

    public string levelToCheck;

    public bool canLoadLevel;

    public GameObject mapPointActive;
    public GameObject mapPointInactive;

    private bool levelUnlocked;

    private bool levelLoading;

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
    }

    private void Update()
    {
        if(Input.GetButtonDown("Jump") && canLoadLevel && levelUnlocked && !levelLoading)
        {
            StartCoroutine("LevelLoadWaiter");
            levelLoading = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canLoadLevel = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canLoadLevel = true;
        }
    }

    public IEnumerator LevelLoadWaiter()
    {
        PlayerController.instance.stopMove = true;
        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(levelName);
    }
}
