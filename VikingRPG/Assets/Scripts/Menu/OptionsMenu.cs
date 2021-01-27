using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        switch (qualityIndex)
        {
            case 0:
                QualitySettings.SetQualityLevel(5);
                break;
            case 1:
                QualitySettings.SetQualityLevel(4);
                break;
            case 2:
                QualitySettings.SetQualityLevel(3);
                break;
            case 3:
                QualitySettings.SetQualityLevel(2);
                break;
            case 4:
                QualitySettings.SetQualityLevel(1);
                break;
            case 5:
                QualitySettings.SetQualityLevel(0);
                break;
        }
    }
}
