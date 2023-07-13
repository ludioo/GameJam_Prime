using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public AudioClip bgmClip;
    private AudioSource audioSource;

    private void Awake()
    {
        // Ensure the BGMManager persists across scenes
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;

        // Play the BGM audio clip
        if (bgmClip != null)
        {
            audioSource.clip = bgmClip;
            audioSource.Play();
        }// Adjust volume based on the current scene
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Adjust volume based on the current scene name
        if (scene.name == "Gameplay")
        {
            audioSource.volume = 0.5f; // Adjust the volume as desired
        }
        else
        {
            audioSource.volume = 1f; // Reset volume to default in other scenes
        }
    }
}
