using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SpriteTile;

namespace Maniac
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<GameManager>();
                return _instance;
            }
        }

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
        [System.NonSerialized]
        public List<PlayerSpawn> Spawns = new List<PlayerSpawn>();
        


        // Use this for initialization
        void Start()
        {
            PlayerSpawn[] spawns = FindObjectsOfType<PlayerSpawn>();

            for (int i = 0; i < spawns.Length; i++)
			{
			    if(spawns[i].Types.Contains(GameType))
                    Spawns.Add(spawns[i]);
			}

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

            Vector3 spawnLoc = Spawns[Random.Range(0, Spawns.Count)].transform.position;
            player = ((GameObject)Instantiate(PlayerPrefab, spawnLoc, Quaternion.identity)).GetComponent<CharacterController>(); ;
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
