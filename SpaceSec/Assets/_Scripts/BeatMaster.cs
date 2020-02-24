using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMaster : MonoBehaviour
{
	public delegate void Beat();
	public static event Beat OnBeat;

	#region SerializedFields
	[SerializeField] float bpm;
	[SerializeField] Sound[] sounds;
	bool[] haveToPlay;
	#endregion
	float timeBetweenBeets;
	public static BeatMaster Instance; 

	void Awake ()
	{
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
		haveToPlay = new bool[sounds.Length];
	}

	private void Start()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		timeBetweenBeets = 60.0f / bpm;
		StartCoroutine("Rhythm");
		PlaySound("MainTheme");
	}

	IEnumerator Rhythm()
	{
		while (true)
		{ 
			if (OnBeat != null)
			{
				OnBeat();
			}
			//PlayTheSounds();
			yield return new WaitForSeconds(timeBetweenBeets);
		}
	}

	public void PlaySound(string audioName)
	{
		bool found = false;
		for(var i = 0; i < sounds.Length; i++)
		{
			if(sounds[i].name == audioName)
			{
				sounds[i].Play();
				found = true;
			}
		}
		if (!found) print("Sound not found: " + audioName);
	}

	private void PlayTheSounds()
	{
		for (var i = 0; i < sounds.Length; i++)
		{
			if (haveToPlay[i])
			{
				sounds[i].Play();
			}
			haveToPlay[i] = false;
		}
	}

	private void Update()
	{
		foreach (Sound s in sounds)
		{
			s.source.pitch = Time.timeScale;
		}
	}
}
