// Filename:            AudioManager.cs
// Author:              Professor Wolfe
// Creation Date:       4/3/2026
// Last Modified:       4/3/2026
// Current Version:     0.3.1
// Previous Version:    N/A
// Project:             Mage-Grow
// Purpose:             Manages scene-wide audio
// Version Changes:     File Created
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _singletonInstance;

    public static AudioManager SingletonInstance {
        get { 
            return _singletonInstance; 
        }
    }

    [SerializeField]
    [Tooltip("The decibel modifier at maximum volume")]
    private float _maximumDecibel = 0;

    [SerializeField]
    [Tooltip("The decibel modifier at minimum volume")]
    private float _minimumDecibel = -80;

    [SerializeField]
    [Tooltip("The audio group for all music sources")]
    private AudioMixerGroup _musicMixerGroup;

    [SerializeField]
    [Tooltip("The audio group for all sound effect sources")]
    private AudioMixerGroup _sfxMixerGroup;


    // Name:        Awake
    // Description: Runs immediately upon the object loading in
    // Parameters:  None
    // Returns:     None
    void Awake()
    {
        // Boilerplate code
        if (_singletonInstance == null)
        {
            _singletonInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
