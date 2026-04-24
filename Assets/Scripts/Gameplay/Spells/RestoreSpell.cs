 /*******************************************************************************
 * Filename: RestoreSpell.cs
 * Author: Tre Hamilton
 * Created On: 4/1/2026
 * Last Modified On: 4/1/2026
 * Version: 0.2.2
 * Project: Mage-Grow
 * Description: Handles the restore spell functionality.
 * Changes: -  Created RestoreSpell class that implements ISpell interface with properties for range, mask, cast origin, and spell name.
 */
using UnityEngine;

public class RestoreSpell : MonoBehaviour, ISpell
{
    
    [SerializeField] 
    private int _range = 8;
    [SerializeField] 
    private LayerMask _mask;
    [SerializeField] 
    private Transform _castOrigin;
    [SerializeField] 
    private string _spellName = "Restore Spell";

    public int Range => _range;
    public Vector3 CastOrigin => _castOrigin.position;
    public LayerMask Mask => _mask;
    public string SpellName => _spellName;

    public bool PerformRaycast() {
        RaycastHit hit;
        if (Physics.Raycast(CastOrigin, transform.up * Range, out hit, Range, Mask)) {
            RestorableObjects restoreable = hit.collider.GetComponent<RestorableObjects>();
            restoreable.HighlightObject();
            return true;
        }
        return false;
    }

    void Update() {
        PerformRaycast();
    }

    public void CastSpell() {
        if (Physics.Raycast(CastOrigin, transform.up * Range, out RaycastHit hit, Range, Mask)) {
            RestorableObjects restoreable = hit.collider.GetComponent<RestorableObjects>();
            if (restoreable != null) {
                restoreable.RestoreObject();
                Debug.Log("Restored object: " + hit.collider.name);
            }
        }
    }
}
