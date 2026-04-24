/*******************************************************************************
 * Filename: InputManager.cs
 * Author: Tre Hamilton
 * Created On: 3/22/2026
 * Last Modified On: 4/1/2026
 * Version: 0.2.2
 * Project: Mage-Grow
 * Description: Handles the input management for the game.
 * Changes: -  Added support for mouse position and middle mouse button input.
 *          -  Added support start and end spell actions.
 */
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour {

    //Singleton boilerplate code
    private static InputManager _singletonInstance;
    public static InputManager SingletonInstance {
        get {
            return _singletonInstance;
        }
    }

    /**
     * Input action fields
     */
    [SerializeField]
    [Tooltip("The input action asset to use for player input")]
    private InputActionAsset _actionMap;

    [SerializeField]
    [Tooltip("The name of the action map to use for player input")]
    private string _actionMapName = "Player";

    /**
     * Input actions  
     */
    private InputAction _moveAction;
    private InputAction _startSpellAction;
    private InputAction _endSpellAction;
    private InputAction _pauseAction;
    private InputAction _changeSpellAction;

    /**
     * Input values
     */
    public Vector2 MoveInput { get; private set; }
    public bool SpellInput { get; private set; }
    public Vector2 MousePosition { get; private set; }



    /**
     * Input enabling fields
     */
    [SerializeField]
    [Tooltip("Whether to enable UI input or not.")]
    private bool _shouldEnableUI = false;

    [SerializeField]
    [Tooltip("Whether to enable gameplay input or not.")]
    private bool _shouldEnableGameplay = true;


    void Awake() {
        if (_singletonInstance == null) {
            _singletonInstance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        _moveAction = _actionMap.FindActionMap(_actionMapName).FindAction("Move");
        _startSpellAction = _actionMap.FindActionMap(_actionMapName).FindAction("StartSpell");
        _endSpellAction = _actionMap.FindActionMap(_actionMapName).FindAction("EndSpell");
        _pauseAction = _actionMap.FindActionMap(_actionMapName).FindAction("TogglePause");
        _changeSpellAction = _actionMap.FindActionMap(_actionMapName).FindAction("ChangeSpell");

        RegisterHeldActions();   
    }

    void Update() {
        MousePosition = Mouse.current.position.ReadValue();
    }
    private void RegisterHeldActions() {
        _moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        _moveAction.canceled += context => MoveInput = Vector2.zero;

    }

    public void RegisterPulseAction(string actionName, Action desiredEvent) {
        Debug.Log("Registering " + actionName + " to " + desiredEvent.Method.Name);
        _actionMap.FindActionMap(_actionMapName).FindAction(actionName).started += context => desiredEvent();
    }

    public void DeregisterPulseAction(string actionName, Action desiredEvent) {
        Debug.Log("Deregistering " + actionName + " to " + desiredEvent.Method.Name);
        _actionMap.FindActionMap(_actionMapName).FindAction(actionName).started -= context => desiredEvent();
    }

    private void OnEnable() {
        if (_shouldEnableUI && _pauseAction != null) {
            _pauseAction.Enable();
        }
        if (_shouldEnableGameplay) {
            _moveAction.Enable();
            _startSpellAction.Enable();
            _endSpellAction.Enable();
            _changeSpellAction.Enable();
        }
    }

    //kept getting null errors
    //https://discussions.unity.com/t/usage-of-null-propagation-on-unity-objects-is-incorrect/880951
    private void OnDisable() {
        _moveAction.Disable();
        _startSpellAction.Disable();
        _endSpellAction.Disable();
        _changeSpellAction.Disable();
        _pauseAction.Disable();
    }

    public void DisableGameplayInput() {
        _moveAction.Disable();
        _startSpellAction.Disable();
        _endSpellAction.Disable();
        _changeSpellAction.Disable();
    }
}
