using UnityEngine;

public class LevelAudioSetter : MonoBehaviour
{
    // Drag the specific music track for THIS level into this slot in the inspector!
    [SerializeField] private AudioClip levelSoundtrack;

    void Start()
    {
        // The moment this specific level scene wakes up, it overrides the global track
        if (AudioManager.instance != null)
        {
            AudioManager.instance.ChangeSoundtrack(levelSoundtrack);
        }
    }
}
