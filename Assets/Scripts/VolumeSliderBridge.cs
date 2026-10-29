using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderBirdge : MonoBehaviour
{
    public void OnSoundtrackSliderChanged(float sliderValue)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.UpdateSoundtrackVolume(sliderValue);
        }
    }

    public void OnSFXSliderChanged(float sliderValue)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.UpdateSFXVolume(sliderValue);
        }
    }
}
