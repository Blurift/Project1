using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SpriteTile;

namespace Maniac
{
    public class GameManager : MonoBehaviour
    {
		public Text ammoText;
        public Text scoreText;
        public GameObject AIManager;
        private int score;
        private bool isRunning = true;

        public TextAsset level;

        //Game Information
        public GameType GameType = GameType.Wave;
        private GameLogic gameLogic;

        //Spawn

        //Player
        public GameObject StartingWeapon;
		public GameObject StartingSecondaryWeapon;
        public GameObject PlayerPrefab;
        private CharacterController player;

        

        // Use this for initialization
        void Start()
        {
            Tile.SetCamera();
            Tile.LoadLevel(level);
            switch (GameType)
            {
                case GameType.Wave:
                    gameLogic = new GameLogicWaves();
                    break;
                case GameType.Conquer:
                    gameLogic = new GameLogicConquer();
                    break;
            }

            gameLogic.Start();

            player = ((GameObject)Instantiate(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity)).GetComponent<CharacterController>(); ;
			player.SetSecondaryWeapon(((GameObject)Instantiate(StartingSecondaryWeapon)).GetComponent<Weapon>());
            player.SetMainWeapon(((GameObject)Instantiate(StartingWeapon)).GetComponent<Weapon>());

        }

        //TO BE REMOVED
        void UpdateScore()
        {
            scoreText.text = "Score: " + score;
        }
		public void SetAmmo(float currentAmmo, float maxAmmo)
		{
			ammoText.text = currentAmmo + "/" + maxAmmo;
		}
        public void AddScore(int newScoreValue)
        {
            score += newScoreValue;
            UpdateScore();
        }

        // Update is called once per frame
        void Update()
        {
            gameLogic.Update(player);
        }

        
    }

    public enum GameType
    {
        Wave,
        Conquer
    }
}
