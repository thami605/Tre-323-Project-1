 /*******************************************************************************
 * Filename: RestorableObjects.cs
 * Author: Tre Hamilton
 * Created On: 4/1/2026
 * Last Modified On: 4/3/2026
 * Version: 0.3.1
 * Project: Mage-Grow
 * Description: handles the end game and victory conditions for the game.
 * Changes: -  Added PlaySFX method to play sound effect when restoring an object.
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryHandler : MonoBehaviour
{
    bool _victoryAchieved;
    private GameObject _victoryScreen;
    private InputManager _inputManager;
    PlantHandler[] _plants;
    void Awake() {
        _victoryScreen = gameObject.transform.GetChild(0).gameObject;
    }
    void Start() {
        _inputManager = InputManager.SingletonInstance;
        _victoryScreen.SetActive(false);
        _victoryAchieved = false;
        _plants = FindObjectsOfType<PlantHandler>();
        Debug.Log("Found " + _plants.Length + " plants in the scene.");
    }
    void Update() {
        if (!_victoryAchieved) {
            CheckVictoryCondition();
        }
    }

    private void CheckVictoryCondition() {
        if (_plants.Length == 0) return; // No plants to check
        
        bool allGrown = true; 
        foreach (PlantHandler plant in _plants) {
            if (!plant.IsGrown) {
                allGrown = false;
            }
        }
        
        if (allGrown) {
            Debug.Log("Victory! All plants are grown.");
            _victoryScreen.SetActive(true);
            _victoryAchieved = true;
            //inputManager.DisableGameplayInput();
        }
    }
}
