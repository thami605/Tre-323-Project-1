/*******************************************************************************
 * Filename: SpellMouseController.cs
 * Author: Tre Hamilton
 * Created On: 4/1/2026
 * Last Modified On: 4/1/2026
 * Version: 0.2.2
 * Project: Mage-Grow
 * Description: Controls the movement of the spell object to follow the mouse 
 *              position when a spell is active.
 */
using UnityEngine;

public class SpellMouseController : MonoBehaviour
{
    private InputManager _inputManager;
    private SpellManager _spellManager;

    private Camera _mainCamera;
    [SerializeField]
    private float _moveHeight = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _inputManager = InputManager.SingletonInstance;
        _spellManager = FindObjectOfType<SpellManager>();
        _mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_spellManager.GetSpellActive()) {
            MoveSpellToMouse();
        }
    }

    private void MoveSpellToMouse() {
        Ray ray = _mainCamera.ScreenPointToRay(_inputManager.MousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.up * _moveHeight);
        if (plane.Raycast(ray, out float distance)) {
            Vector3 targetPoint = ray.GetPoint(distance);
            transform.position = targetPoint;
        }
    }
}
