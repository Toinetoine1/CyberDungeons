using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    private Resolution[] _resolutions;
    public Dropdown resolutionDropdown;
    public Dropdown graphicDropdown;


    private void Start()
    {
        graphicDropdown.value = graphicDropdown.options.Count - 1;
        QualitySettings.SetQualityLevel(graphicDropdown.value);
        graphicDropdown.RefreshShownValue();
        
        _resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currrenResolutionIndex = 0;
        int i = 0;
        foreach (var resolution in _resolutions)
        {
            string newOption = resolution.width + " x " + resolution.height;
            options.Add(newOption);
            if (resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height)
            {
                currrenResolutionIndex = i;
            }
            i++;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currrenResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void PlaySound()
    {
        FindObjectOfType<AudioManager>().Play("MenuButton");
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex); // 0 is very low, 1 is low ...
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void OpenWebsite()
    {
        Application.OpenURL("www.cyberdungeons.fr");
    }

}
