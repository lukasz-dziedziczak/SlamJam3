using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_GameSounds : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip openingSound;
    [SerializeField] AudioClip completeSound;

    public void PlayOpeningSound()
    {
        if (audioSource != null)
        {
            audioSource.clip = openingSound;
            audioSource.Play();
        }
    }

    public void PlayCompleteSound()
    {
        if (audioSource != null)
        {
            audioSource.clip = completeSound;
            audioSource.Play();
        }
    }
}
