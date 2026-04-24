/*******************************************************************************
 * Filename: MainMenu.cs
 * Author: Tre Hamilton
 * Created On: 3/22/2026
 * Last Modified On: 3/22/2026
 * Version: 0.1.2
 * Project: Mage-Grow
 * Description: Handles the main menu UI and its interactions.
 */
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenu;

    [SerializeField]
    private GameObject _optionsMenu;

    [SerializeField]
    private GameObject _levelsMenu;


    void Start() {
        _mainMenu.SetActive(true);
        _optionsMenu.SetActive(false);
        _levelsMenu.SetActive(false);
    }

    /**
     * To other menus
     */
    public void MainToOptions() {
        _mainMenu.SetActive(false);
        _optionsMenu.SetActive(true);
    }

    public void MainToLevels() {
        _mainMenu.SetActive(false);
        _levelsMenu.SetActive(true);
    }

    /**
     * Back to Main Menu
     */
    public void OptionsToMain() {
        _optionsMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void LevelsToMain() {
        _levelsMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void ExitGame() {
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
