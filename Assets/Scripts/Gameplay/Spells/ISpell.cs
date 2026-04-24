/*******************************************************************************
 * Filename: ISpell.cs
 * Author: Tre Hamilton
 * Created On: 3/22/2026
 * Last Modified On: 3/28/2026
 * Version: 0.2.1
 * Project: Mage-Grow
 * Description: Defines the interface for spell objects.
 * Changes: - Added ISpell interface with necessary properties and methods for spells.
 */
using UnityEngine;

public interface ISpell {

    int Range { get; }
    Vector3 CastOrigin { get; }
    LayerMask Mask { get; }
    string SpellName { get; }

    public bool PerformRaycast() {
        return false;
    }

    public void CastSpell() {
        
    }
}

