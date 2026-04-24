/*******************************************************************************
 * Filename: DestroySpell.cs
 * Author: Tre Hamilton
 * Created On: 3/22/2026
 * Last Modified On: 4/1/2026
 * Version: 0.2.2
 * Project: Mage-Grow
 * Description: Implements the destroy spell functionality.
 * Changes: - Updated to include raycast highlighting and destruction of objects.
 */
using UnityEngine;

public class DestroySpell : MonoBehaviour, ISpell 
{
    [SerializeField] 
    private int _range = 8;
    [SerializeField] 
    private LayerMask _mask;
    [SerializeField] 
    private Transform _castOrigin;
    [SerializeField] 
    private string _spellName = "Destroy Spell";

    public int Range => _range;
    public Vector3 CastOrigin => _castOrigin.position;
    public LayerMask Mask => _mask;
    public string SpellName => _spellName;

    public bool PerformRaycast() {
        RaycastHit hit;
        if (Physics.Raycast(CastOrigin, transform.up * Range, out hit, Range, Mask)) {
            DestroyableObjects destroyable = hit.collider.GetComponent<DestroyableObjects>();
            destroyable.HighlightObject();
            return true;
        }
        return false;
    }

    void Update() {
        PerformRaycast();
    }

    public void CastSpell() {
        if (Physics.Raycast(CastOrigin, transform.up * Range, out RaycastHit hit, Range, Mask)) {
            DestroyableObjects destroyable = hit.collider.GetComponent<DestroyableObjects>();
            if (destroyable != null) {
                destroyable.DestroyObject(destroyable.gameObject);
                Debug.Log("Destroyed object: " + hit.collider.name);
            }
        }

    }

    

}
