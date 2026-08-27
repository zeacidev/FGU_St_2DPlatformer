using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // 1. SPLIT IT: We create two separate channels in the Inspector
    [SerializeField] private AudioSource sfxSource;       // For one-shots (Death, Coins, Jump)
    [SerializeField] private AudioSource soundtrackSource; // ONLY for background music

    [SerializeField] private AudioClip BackgroundMusic;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlaySoundtrack();
    }

    // 2. Play sound effects on the dedicated SFX speaker channel
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip); // This channel NEVER pauses!
        }
    }

    // 3. Assign and loop music on the soundtrack speaker channel
    public void PlaySoundtrack()
    {
        if (BackgroundMusic != null)
        {
            soundtrackSource.clip = BackgroundMusic;
            soundtrackSource.loop = true;
            soundtrackSource.Play();
        }
    }

    // 4. Pauses ONLY the soundtrack speaker, leaving your SFX channel perfectly awake!
    public void PauseSoundtrack()
    {
        soundtrackSource.Pause();
    }

    public void ResumeSoundtrack()
    {
        soundtrackSource.UnPause();
    }

    public void ChangeSoundtrack(AudioClip newMusic)
    {
        if (soundtrackSource.clip == newMusic) return;

        if (newMusic != null)
        {
            BackgroundMusic = newMusic;
            soundtrackSource.clip = newMusic;
            soundtrackSource.loop = true;
            soundtrackSource.Play();
        }
    }
}
