using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip mainMenuClip;
    public AudioClip gameMenuClip;
    public AudioClip bossMenuClip;

    private AudioSource _source;
    
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        GameEvents.MainSceneLoading += MainSceneLoadingHandler;
    }

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();

        _source.clip = mainMenuClip;
        _source.Play();
    }

    private void MainSceneLoadingHandler(object sender, EventArgs args)
    {
        _source.clip = gameMenuClip;
        _source.Play();
    }
}
