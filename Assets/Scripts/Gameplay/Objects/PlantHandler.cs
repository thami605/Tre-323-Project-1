/*******************************************************************************
 * Filename: PlantHandler.cs
 * Author: Tre Hamilton
 * Created On: 4/3/2026
 * Last Modified On: 4/3/2026
 * Version: 0.3.1
 * Project: Mage-Grow
 * Description: Handles the behavior and logic for the plant objects in the game.
 */
using UnityEngine;
using System.Collections; // Needed for IEnumerator and WaitForSeconds

public class PlantHandler : MonoBehaviour
{
    
    private bool _isGrown;
    private bool _inPositionToGrow;
    [SerializeField]
    private GameObject[] _growthStages;
    [SerializeField]
    private Transform _sunlight;

    //Used to communicate _isGrown's value to VictoryHandler
    public bool IsGrown => _isGrown;


    void Awake()
    {
        _isGrown = false;
    }

    void FixedUpdate() {
        if (_inPositionToGrow && !_isGrown && CheckForSunlight()) {
            StartCoroutine(Grow());
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Sunlight")) {
            _inPositionToGrow = true;
        }
    }

    private bool CheckForSunlight() {
        RaycastHit hit;
        //Raycast to check for sunlight
        if (Physics.Linecast(transform.position + Vector3.up * 0.5f, _sunlight.position, out hit)) {
            Debug.DrawLine(transform.position + Vector3.up * 0.5f, _sunlight.position, Color.red);
            Debug.Log("Sunlight is blocked by: " + hit.collider.gameObject.name);
            return false;
        }
        return true;
    }
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Sunlight")) {
            _inPositionToGrow = false;
        }
    }
    //Wanted multiple growth stages
    //https://discussions.unity.com/t/triggering-a-function-every-x-seconds/390382/4
    //https://docs.unity3d.com/ScriptReference/WaitForSeconds.html
    private IEnumerator Grow() {
        yield return new WaitForSeconds(2);
        _growthStages[0].SetActive(false);
        _growthStages[1].SetActive(true);
        yield return new WaitForSeconds(2);
        _growthStages[1].SetActive(false);
        _growthStages[2].SetActive(true);
        yield return new WaitForSeconds(2);
        _isGrown = true;
    }
}
