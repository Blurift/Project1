﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

	private static SoundManager _instance;
	public static SoundManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<SoundManager>();
			}
			return _instance;
		}
	}
	private List<AudioSource> freeSources = new List<AudioSource>();
	private List<AudioSource> activeSources = new List<AudioSource>(); 
	public int audioPoolSize = 10;
	// Use this for initialization
	void Start () 
	{
		for (int i = 0; i < audioPoolSize; i++)
		{
			GameObject go = new GameObject("audio");
			go.AddComponent<AudioSource>();

			freeSources.Add(go.GetComponent<AudioSource>());
		}
	
	}

	public void PlayIt(AudioClip sound, bool loop, Vector3 position)
	{
		if (freeSources.Count == 0) 
		{

			freeSources.Add(gameObject.AddComponent<AudioSource>());  
			freeSources[freeSources.Count - 1].loop = loop;
			freeSources[freeSources.Count-1].clip = sound;
			freeSources[freeSources.Count-1].Play();
		}
		freeSources [0].transform.position = position;
		freeSources [0].loop = loop;
		freeSources [0].clip = sound;
		activeSources.Add(freeSources [0]);
		freeSources [0].Play();
		freeSources.RemoveAt(0);


	} 
	
	// Update is called once per frame
	void Update () 
	{
		for (int i=activeSources.Count - 1; i> -1; i--) 
		{
			AudioSource audio = activeSources[i];
			if (!audio.isPlaying)
			{
				audio.transform.position = this.transform.position;
				freeSources.Add(audio);
				activeSources.Remove(audio);
			}
		}
	
	
	}
}
