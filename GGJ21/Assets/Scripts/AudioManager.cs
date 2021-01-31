using UnityEngine;
using UnityEngine.Audio;

public enum AudioType
{
    Aux01,
    Aux02,
    Aux03
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }


    public AudioMixerSnapshot menuSnapshot;
    public AudioMixerSnapshot idleSnapshot;
    public AudioMixerSnapshot transitionSnapshot;
    public AudioMixerSnapshot aux01Snapshot;
    public AudioMixerSnapshot aux02Snapshot;
    public AudioMixerSnapshot aux03Snapshot;

    public AudioSource menuAudioSource;
    public AudioSource transitionAudioSource;
    public AudioSource backgrundMusicAudioSource;
    public AudioSource aux01AudioSource;
    public AudioSource aux02AudioSource;
    public AudioSource aux03AudioSource;

    private bool startPlaying = false;

    void OnEnable()
    {
        Instance = this;
    }

    void OnDisable()
    {
        if (Instance == this) Instance = null;
    }

    public void OnStartPressed()
    {
        startPlaying = true;
        menuAudioSource.loop = false;
        
    }

    private void Update()
    {
        if (startPlaying)
        {
            if (!menuAudioSource.isPlaying)
            {
                startPlaying = false;
                transitionAudioSource.Play();
                backgrundMusicAudioSource.Play();
                aux01AudioSource.Play();
                aux02AudioSource.Play();
                aux03AudioSource.Play();
                idleSnapshot.TransitionTo(-0.1f);
            }
        }
    }

    public void CheckColiision(AudioType type)
    {
        switch(type)
        {
            case AudioType.Aux01:
                aux01Snapshot.TransitionTo(0.5f);
                break;
            case AudioType.Aux02:
                aux02Snapshot.TransitionTo(0.5f);
                break;
            case AudioType.Aux03:
                aux03Snapshot.TransitionTo(0.5f);
                break;
        }
    }
}
