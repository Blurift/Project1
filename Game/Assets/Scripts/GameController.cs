using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public GameObject player;
	public Text restartText;
	public Text scoreText;
	public GameObject AIManager;
	private int score;
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
		if (player.GetComponent<Health> ().isAlive == Health.State.Dead) 
		{
			AIManager.GetComponent<AIManager>().Disable();
			restartText.text = "Press 'R' for Restart";
			if (Input.GetKeyDown (KeyCode.R)) 
			{
				Application.LoadLevel (Application.loadedLevel);
			}

		}
	
	}
}
