/*******************************************************************************
 * Filename: DestroySpell.cs
 * Author: Tre Hamilton
 * Created On: 3/22/2026
 * Last Modified On: 3/28/2026
 * Version: 0.2.1
 * Project: Mage-Grow
 * Description: Implements the destroy spell functionality.
 * Changes: - Added fields for spell range, mask, and cast origin.
 *          - Updated PerformRaycast to use new fields and check for DestroyableObjects component.
 *          - Added ISpell interface with necessary properties and methods for spells.
 */
using UnityEngine;

public class DestroyableObjects : MonoBehaviour
{
    private bool _isHighlighted = false;
    private Material _originalMaterial;
    [SerializeField] 
    private Material _highlightMaterial;
    [SerializeField] 
    private AudioClip _destroySound;
    private AudioSource _audioSource;
    private Renderer _objectRenderer;

    void Start() {
        _objectRenderer = GetComponent<Renderer>();
        _audioSource = GetComponent<AudioSource>();
    }
    public void DestroyObject(GameObject obj) {
        Destroy(obj);
        PlaySFX();
    }

    private void PlaySFX() {
        if (_destroySound != null) {
            AudioSource.PlayClipAtPoint(_destroySound, transform.position);
        }
    }

    void Update() {
        if (_isHighlighted) {
            UnhighlightObject();
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
}
