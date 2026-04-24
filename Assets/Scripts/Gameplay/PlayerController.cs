/*******************************************************************************
 * Filename: PlayerController.cs
 * Author: Tre Hamilton
 * Created On: 3/22/2026
 * Last Modified On: 4/1/2026
 * Version: 0.2.2
 * Project: Mage-Grow
 * Description: Handles the player movement and animations.
 * Changes: -  Added support for enabling and disabling player control.
 */
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private InputManager _inputManager;
    private Animator _animator;

    private Rigidbody _playerRigidBody;
    private float _velocity;
    private bool _hasControl;

    [SerializeField]
    [Tooltip("The speed at which the player moves.")] 
    private float _speed = 5f;

    [SerializeField]
    [Tooltip("The speed at which the player rotates.")]
    private float _rotationSpeed = 30f;

    void Awake() {
        _hasControl = true;
    }

    void Start() {
        _inputManager = InputManager.SingletonInstance;
        _animator = GetComponent<Animator>();
        _playerRigidBody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update() {
        if(_hasControl) {
            HandleMovement();
        }
        _animator.SetFloat("Velocity", _velocity);
    }

    private void HandleMovement() {
        transform.Translate(Vector3.forward * Time.deltaTime * _inputManager.MoveInput.y * _speed);
        transform.Rotate(Vector3.up * Time.deltaTime * _inputManager.MoveInput.x * _speed * _rotationSpeed);
        _velocity = _inputManager.MoveInput.magnitude;
        transform.position = new Vector3(transform.position.x, 0.025f, transform.position.z);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    public void _enableControl() {
        _hasControl = true;
    }

    public void _disableControl() {
        _hasControl = false;
    }
}
