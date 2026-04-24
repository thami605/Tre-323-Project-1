/*******************************************************************************
 * Filename: PauseHandler.cs
 * Author: Tre Hamilton
 * Created On: 3/22/2026
 * Last Modified On: 4/3/2026
 * Version: 0.3.1
 * Project: Mage-Grow
 * Description: Handles the pause menu UI and its interactions.
 * Changes: -  Added reset level functionality to the pause menu.
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour
{
    private InputManager _inputManager;

    [SerializeField]
    [Tooltip("The pause menu to display when the game is paused.")]
    private GameObject _pauseMenu;



     void Awake() {
        _inputManager = InputManager.SingletonInstance;
        _inputManager.RegisterPulseAction("TogglePause", ToggleMenu);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _pauseMenu.SetActive(false);
    }

    public void ToggleMenu() {
        if (!_pauseMenu.activeSelf) {
            _pauseMenu.SetActive(true);
        } else {
            _pauseMenu.SetActive(false);
        }
    }

    public void Resetlevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ToggleMenu();
    }

    public void ExitLevel() {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDestroy() {
        _inputManager.DeregisterPulseAction("TogglePause", ToggleMenu);
    }
}
