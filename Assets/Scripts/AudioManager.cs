using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    
    [SerializeField] AudioClip _startingSong;

    //Singleton Pattern
    public static AudioManager Instance = null;

    AudioSource _audioSource;

    private void Start()
    {
        //play starting song when AudioManager starts
        if (_startingSong != null)
        {
            PlaySong(_startingSong);
        }
    }

    private void Awake()
    {
        #region Singleton Pattern (Simple)
        if(Instance == null)
        {
            //doesn't exist yet, this is now our singleton!
            Instance = this;
            DontDestroyOnLoad(this);
            // fill references
            _audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    public void PlaySong(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
