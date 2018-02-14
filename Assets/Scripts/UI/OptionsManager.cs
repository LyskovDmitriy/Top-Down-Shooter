using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour 
{

	public AudioMixer audioMixer;
	public AudioManager audioManager;
	public Dropdown resolutionDropdown;
	public Toggle fullscreenToggle;
	public Slider masterVolumeSlider;
	public Slider musicVolumeSlider;
	public Slider sfxVolumeSlider;


	private List<Resolution> availableResolutions;


	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}


	public void ChangeResolution(int resolutionIndex)
	{
		Screen.SetResolution(availableResolutions[resolutionIndex].width, availableResolutions[resolutionIndex].height, 
			Screen.fullScreen);
		Debug.Log(Screen.currentResolution.width + " " + Screen.currentResolution.height);
	}


	public void ChangeFullscreenState(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}


	public void SetMasterVolume(float volume)
	{
		audioMixer.SetFloat("MasterVolume", volume);
	}


	public void SetMusicVolume(float volume)
	{
		audioMixer.SetFloat("MusicVolume", volume);
	}


	public void SetSFXVolume(float volume)
	{
		audioMixer.SetFloat("SFXVolume", volume);
	}

	
	void Awake () 
	{
		availableResolutions = Screen.resolutions.ToList();
		Resolution currentResolution = new Resolution();
		currentResolution.height = Screen.height;
		currentResolution.width = Screen.width;
		resolutionDropdown.ClearOptions();
		List<string> resolutionStrings = new List<string>(availableResolutions.Count);
		int currentIndex = 0;

		for (int i = 0; i < availableResolutions.Count; i++)
		{
			resolutionStrings.Add(availableResolutions[i].width + " X " + availableResolutions[i].height);
			if (availableResolutions[i].width == currentResolution.width && 
				availableResolutions[i].height == currentResolution.height)
			{
				currentIndex = i;
			}
		}

		resolutionDropdown.AddOptions(resolutionStrings);
		resolutionDropdown.value = currentIndex;
		resolutionDropdown.onValueChanged.AddListener(ChangeResolution);

		fullscreenToggle.isOn = Screen.fullScreen;
	}


	void Start()
	{
		CursorController.instance.SetDefaultCursor();
		float masterVolume = 0;
		float musicVolume = 0;
		float sfxVolume = 0;
		if (PlayerPrefs.HasKey("MasterVolume"))
		{
			masterVolume = PlayerPrefs.GetFloat("MasterVolume");
		}
		if (PlayerPrefs.HasKey("MusicVolume"))
		{
			musicVolume = PlayerPrefs.GetFloat("MusicVolume");
		}
		if (PlayerPrefs.HasKey("SFXVolume"))
		{
			sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
		}
		masterVolumeSlider.value = masterVolume;
		musicVolumeSlider.value = musicVolume;
		sfxVolumeSlider.value = sfxVolume;
	}


	void OnDestroy()
	{
		audioManager.SetVolumeSetting();
	}
}
