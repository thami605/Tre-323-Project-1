/*******************************************************************************
 * Filename: SpellUIManager.cs
 * Author: Tre Hamilton
 * Created On: 4/1/2026
 * Last Modified On: 4/3/2026
 * Version: 0.3.1
 * Project: Mage-Grow
 * Description: Manages the spell UI elements and updates them based on player 
 *              input.
 * Changes: -  Changed toggling spells to be based on current spell index
 *             insted of input from input manager. More accurate to the actual
 *             spell being used.
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpellUIManager : MonoBehaviour
{
    private InputManager _inputManager;
    private SpellManager _spellManager;

    [SerializeField]
    private GameObject _spellUI;

    [SerializeField]
    private GameObject _spellCover0;

    [SerializeField]
    private GameObject _spellCover1;

    void Awake() {
        _spellUI = gameObject;
    }

    void Start() {
        if (SceneManager.GetActiveScene().name != "MainMenu") {
            _spellUI.SetActive(true);
        } else {
            _spellUI.SetActive(false);
        }
        _inputManager = InputManager.SingletonInstance;  
        _spellManager = FindObjectOfType<SpellManager>();     
        _spellCover0.SetActive(false);
        _spellCover1.SetActive(true);
        _inputManager.RegisterPulseAction("ChangeSpell", ToggleSpellUI);
    }
    private void ToggleSpellUI() {
        switch (_spellManager.GetCurrentSpellIndex()) {
            case 1:
                _spellCover0.SetActive(true);
                _spellCover1.SetActive(false);
                break;
            default:
                _spellCover0.SetActive(false);
                _spellCover1.SetActive(true);
                break;
        }
    }
}
