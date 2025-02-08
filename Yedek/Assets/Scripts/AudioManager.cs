using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource audioSource;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
    }

    public void PlayEffect(AudioClip effectClip)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(effectClip);
        }
    }

   public void PlayEffect(AudioClip effectClip, Action onComlate)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(effectClip);
        }

    }

}
