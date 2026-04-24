/*******************************************************************************
 * Filename: LevelSelection.cs
 * Author: Tre Hamilton
 * Created On: 3/22/2026
 * Last Modified On: 4/3/2026
 * Version: 0.3.1
 * Project: Mage-Grow
 * Description: Handles the level selection UI and its interactions.
 * Changes: -  Added functionality to load the next level in the build settings.
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public void LoadLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }

    public void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(nextSceneIndex);
        } else {
            Debug.Log("No more levels to load. Returning to main menu.");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
