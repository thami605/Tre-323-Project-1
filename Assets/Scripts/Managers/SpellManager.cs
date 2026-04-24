/*******************************************************************************
 * Filename: SpellManager.cs
 * Author: Tre Hamilton
 * Created On: 3/22/2026
 * Last Modified On: 4/3/2026
 * Version: 0.3.1
 * Project: Mage-Grow
 * Description: Handles the spell management for the game.
 * Changes: -  Added support for managing multiple spells and switching between them.
 */
using UnityEngine;
using System.Collections.Generic;

public class SpellManager : MonoBehaviour {

    private static SpellManager _singletonInstance;
    private InputManager _inputManager;
    [SerializeField]
    private PlayerController _playerController;

    private int _currentSpellIdx;
    public List<ISpell> spells = new List<ISpell>();
    private bool spellActive;

    [SerializeField] private GameObject _spellIndexZero;
    [SerializeField] private GameObject _spellIndexOne;

    
    void Awake() {
        _playerController = FindObjectOfType<PlayerController>();

        _currentSpellIdx = 0;
        spellActive = false;
        
        spells.Add(_spellIndexZero.GetComponent<ISpell>());
        _spellIndexZero.SetActive(false);
        spells.Add(_spellIndexOne.GetComponent<ISpell>());
        _spellIndexOne.SetActive(false);
    }

    void Start() {
        _inputManager = InputManager.SingletonInstance;
        _inputManager.RegisterPulseAction("StartSpell", InitiateSpell);
        _inputManager.RegisterPulseAction("ChangeSpell", ChangeSpell);
        _inputManager.RegisterPulseAction("EndSpell", EndSpell);
     }
     void OnDestroy() {
        _inputManager.DeregisterPulseAction("StartSpell", InitiateSpell);
        _inputManager.DeregisterPulseAction("ChangeSpell", ChangeSpell);
        _inputManager.DeregisterPulseAction("EndSpell", EndSpell);
     }


    public void InitiateSpell() {
        if (!spellActive) {
            spellActive = true;
            // Activate the current spell object
            if (_currentSpellIdx == 0) {
                _spellIndexZero.SetActive(true);
            } else if (_currentSpellIdx == 1) {
                _spellIndexOne.SetActive(true);
            }
            _playerController._disableControl();
        }
        Debug.Log("Cast spell: " + spells[_currentSpellIdx].SpellName);
    }

    public void EndSpell() {
        if (spellActive) {
            if (spells[_currentSpellIdx].PerformRaycast()) {
                Debug.Log("Spell hit something!");
                spells[_currentSpellIdx].CastSpell();
            } else {
                Debug.Log("Spell did not hit anything.");
            }
            spellActive = false;
            // Deactivate the current spell object
            if (_currentSpellIdx == 0) {
                _spellIndexZero.SetActive(false);
            } else if (_currentSpellIdx == 1) {
                _spellIndexOne.SetActive(false);
            }
            _playerController._enableControl();
        }
    }

    private void ChangeSpell() {
        if (!spellActive) {
            if (_currentSpellIdx == 0) {
                _currentSpellIdx = 1;
            } else {
                _currentSpellIdx = 0;
            }
        }
    }

    public int GetCurrentSpellIndex() {
        return _currentSpellIdx;
    }
    public bool GetSpellActive() {
        return spellActive;
    }
}
