using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

	public bool canPlayMusic;


	[SerializeField] private AudioMixer audioMixer;
	[SerializeField] private AudioSource backgroundMusicSource;
	[SerializeField] private AudioSource playerHurtAudio;
	[SerializeField] private AudioSource playerDeathAudio;
	[SerializeField] private AudioClip[] musicClips;


	public void PlayerHurt()
	{
		if (!playerHurtAudio.isPlaying)
		{
			playerHurtAudio.Play();
		}
	}


	public void PlayerDeath()
	{
		playerDeathAudio.Play();
	}


	public void SetVolumeSetting()
	{
		float masterVolume = 0;
		float musicVolume = 0;
		float sfxVolume = 0;
		audioMixer.GetFloat("MasterVolume", out masterVolume);
		audioMixer.GetFloat("MusicVolume", out musicVolume);
		audioMixer.GetFloat("SFXVolume", out sfxVolume);
		PlayerPrefs.SetFloat("MasterVolume", masterVolume);
		PlayerPrefs.SetFloat("MusicVolume", musicVolume);
		PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
	}


	public void GetAndApplyVolumeSettings()
	{
		if (PlayerPrefs.HasKey("MasterVolume"))
		{
			audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
		}
		if (PlayerPrefs.HasKey("MusicVolume"))
		{
			audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
		}
		if (PlayerPrefs.HasKey("SFXVolume"))
		{
			audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
		}
	}


	public void PlayRandomMusic()
	{
		AudioClip clipToPlay = musicClips[Random.Range(0, musicClips.Length)];
		backgroundMusicSource.PlayOneShot(clipToPlay);
	}


	void Start () 
	{
		GetAndApplyVolumeSettings();

		if (canPlayMusic)
		{
			PlayRandomMusic();
		}
	}


	void Update()
	{
		if (canPlayMusic)
		{
			if (!backgroundMusicSource.isPlaying)
			{
				PlayRandomMusic();
			}
		}
	}
}
