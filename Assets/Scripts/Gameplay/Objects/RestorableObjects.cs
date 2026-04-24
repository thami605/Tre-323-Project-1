 /*******************************************************************************
 * Filename: RestorableObjects.cs
 * Author: Tre Hamilton
 * Created On: 4/1/2026
 * Last Modified On: 4/3/2026
 * Version: 0.3.1
 * Project: Mage-Grow
 * Description: Handles the functionality for objects that can be restored.
 * Changes: -  Created RestorableObjects class with methods for highlighting, 
 *             unhighlighting, destroying, and restoring objects, as well as 
 *             playing sound effects on destruction.
 *          -  Added collision detection to stop restoring if path is blocked
 *          -  Changed sound to be different than DestroyableObjects
 */
using UnityEngine;

public class RestorableObjects : MonoBehaviour
{
    private bool _isHighlighted = false;
    private Material _originalMaterial;
    [SerializeField] 
    private Material _highlightMaterial;
    [SerializeField] 
    private AudioClip _restoreSound;
    [SerializeField]
    private Transform _RestoredPosition; 
    private Vector3 _originalPosition;
    private AudioSource _audioSource;
    private Renderer _objectRenderer;

    private bool _isCurrentlyRestoring;
    private bool _isBlocked;

    void Start() {
        _objectRenderer = GetComponent<Renderer>();
        _originalPosition = transform.position;
        _audioSource = GetComponent<AudioSource>();
        _isHighlighted = false;
        _isCurrentlyRestoring = false;
    }

    void Update() {
        //Update material back to original if highlighted
        if (_isHighlighted) {
            UnhighlightObject();
        }
        //If restoring, move towards restored position, if blocked, move back to original position
        if (_isCurrentlyRestoring && Vector3.Distance(transform.position, _RestoredPosition.position) > 0.09f) {
            if (Vector3.Distance(transform.position, _RestoredPosition.position) < 0.1f) {
                transform.position = _RestoredPosition.position;
                _originalPosition = transform.position;
                _isCurrentlyRestoring = false;
                PlaySFX();
            }
            // i like the Lerp function for smooth movement
            //https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Vector3.Lerp.html
            transform.position = Vector3.Lerp(transform.position, _RestoredPosition.position, Time.deltaTime);
        }
        if (_isBlocked) {
            transform.position = Vector3.Lerp(transform.position, _originalPosition, Time.deltaTime);
            if (Vector3.Distance(transform.position, _originalPosition) < 0.1f) {
                _isBlocked = false;
            }
        }
    }
    //Check for collision with destroyable objects while restoring, if colliding, stop restoring and move back to original position
    //https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Collider.OnTriggerEnter.html
    void OnTriggerEnter(Collider other) {
        //Check for collisions when restorying position. Had to make tags to avoid blocking on other objects like the floor
        //https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Component.CompareTag.html 
        if (other.CompareTag("Destroyable")) {
            _isCurrentlyRestoring = false;
            _isBlocked = true;
            Debug.Log("Path blocked by object: " + other.name);
        }
    }

    private void PlaySFX() {
        if (_restoreSound != null) {
            AudioSource.PlayClipAtPoint(_restoreSound, transform.position);
        }
    }

    public void HighlightObject() {
        if (!_isHighlighted) {
            _originalMaterial = _objectRenderer.material;
            _objectRenderer.material = _highlightMaterial;
            _isHighlighted = true;
        }
    }

    public void UnhighlightObject() {
        if (_isHighlighted) {
            _objectRenderer.material = _originalMaterial;
            _isHighlighted = false;
        }
    }

    public void RestoreObject() {
        if (_isCurrentlyRestoring && _isBlocked) return; // Prevent multiple restore calls
        else if (Vector3.Distance(transform.position, _RestoredPosition.position) > 0.1f) {
            _isCurrentlyRestoring = true;
            PlaySFX();
        }
    }
}
