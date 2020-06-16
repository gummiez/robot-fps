using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    string currentLevel, currentLvlMusic;

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	void Awake()
	{
        #region Singleton Pattern

        if (instance == null)
        {
			DontDestroyOnLoad(gameObject);
			instance = this;
        }else if(instance != null)
        {
			Destroy(gameObject);
        }
        #endregion

        foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

    private void Start()
    {
        Play("MenuMusic");
        currentLevel = SceneManager.GetActiveScene().name;
        currentLvlMusic = "MenuMusic";
    }

    private void Update()
    {
        PlayBGMusic();
    }

    void PlayBGMusic()
    {
        if(currentLevel != SceneManager.GetActiveScene().name)
        {
            currentLevel = SceneManager.GetActiveScene().name;
            StopPlaying(currentLvlMusic);
            GetCurrentBGMusic();
            Play(currentLvlMusic);
        }
    }

    private void GetCurrentBGMusic()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        if(activeScene == "MainMenu")
        {
            currentLvlMusic = "MenuMusic";
        }else if(activeScene == "GameScene")
        {
            currentLvlMusic = "Level1";
        }
    }

    public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Stop();
    }

}
