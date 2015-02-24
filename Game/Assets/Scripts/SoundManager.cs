using UnityEngine;
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
		freeSources.Capacity = audioPoolSize;
		activeSources.Capacity = audioPoolSize;
		for (int i = 0; i < audioPoolSize; i++)
		{
			GameObject go = new GameObject("audio");
			go.AddComponent<AudioSource>();

			freeSources.Add(go.GetComponent<AudioSource>());
		}
	
	}

	public void PlayIt(AudioClip sound, bool loop, Vector3 position)
	{
		
		foreach (AudioSource audio in freeSources) 
		{
			if (!audio.isPlaying)
			{
				audio.transform.position = position;
				audio.loop = loop;
				audio.clip = sound;
				activeSources.Add(audio);
				freeSources.Remove(audio);
				audio.Play();
				return;
			}
		}
		
		freeSources.Add(gameObject.AddComponent<AudioSource>());  
		freeSources[freeSources.Count - 1].loop = loop;
		freeSources[freeSources.Count-1].clip = sound;
		freeSources[freeSources.Count-1].Play();
		
	} 
	
	// Update is called once per frame
	void Update () 
	{
		foreach (AudioSource audio in activeSources) 
		{
			if (!audio.isPlaying)
			{
				audio.transform.position = this.transform.position;
				freeSources.Add(audio);
				activeSources.Remove(audio);
			}
		}
	
	}
}
