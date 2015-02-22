using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public GameObject player;
	public Text scoreText;
	public GameObject AIManager;
	public GameObject deathScreen;
	private int score;
	private bool isRunning = true;
	// Use this for initialization
	void Start () 
	{
		score = 0;
		UpdateScore ();
	}
	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	// Update is called once per frame
	void Update () 
	{
		if (player.GetComponent<Health> ().isAlive == Health.State.Dead && isRunning) 
		{
			AIManager.GetComponent<AIManager>().Disable();
			Instantiate(deathScreen);
			isRunning = false;
		}
	
	}
}
