using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Clips")]
    public AudioClip pulseClip;
    public AudioClip gravityClip;
    public AudioClip phaseClip;
    public AudioClip deathClip;

    private void Awake()
    {
        Instance = this;

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayPulse()
    {
        Play(pulseClip, 1f);
    }

    public void PlayGravity()
    {
        Play(gravityClip, 0.85f);
    }

    public void PlayPhase()
    {
        Play(phaseClip, 0.9f);
    }

    public void PlayDeath()
    {
        Play(deathClip, 1f);
    }

    private void Play(AudioClip clip, float volume)
    {
        if (clip == null || audioSource == null) return;
        audioSource.PlayOneShot(clip, volume);
    }
}





