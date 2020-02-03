using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour
{
    public GameObject loadingImage;

    public GameObject mainMenu;
    public GameObject selectLevel;
    public GameObject settings;

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        selectLevel.SetActive(false);
        settings.SetActive(false);
    }

    public void OpenLevelSelect() 
    {
        mainMenu.SetActive(false);
        selectLevel.SetActive(true);
        settings.SetActive(false);
    }

    public void OpenSettings() 
    {
        mainMenu.SetActive(false);
        selectLevel.SetActive(false);
        settings.SetActive(true);
    }

    public void LoadScene(int level) 
    {
        loadingImage.SetActive(true);
        SceneManager.LoadScene(level);
    }

}
