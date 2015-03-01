using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public Text scoreText;
	public GameObject AIManager;
	public GameObject deathScreen;
	private int score;
	private bool isRunning = true;

    public GameObject StartingWeapon;
    public GameObject PlayerPrefab;
    private CharacterController player;

	// Use this for initialization
	void Start () 
	{
        player = ((GameObject)Instantiate(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity)).GetComponent<CharacterController>(); ;
        player.SetWeapon(((GameObject)Instantiate(StartingWeapon)).GetComponent<Weapon>());

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
