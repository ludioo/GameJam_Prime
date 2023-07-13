using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; set; }

    public AudioSource audioSource;
    public AudioClip paperSfx;
    public AudioClip buttonSfx;
    public AudioClip spotlightSfx;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }
    public void PlayPaper()
    {
        audioSource.PlayOneShot(paperSfx);
    }

    public void PlayButton()
    {
        audioSource.PlayOneShot(buttonSfx);
    }

    public void PlaySpotlight()
    {
        audioSource.PlayOneShot(spotlightSfx);
    }
}
