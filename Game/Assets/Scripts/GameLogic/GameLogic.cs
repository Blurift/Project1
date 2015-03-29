using UnityEngine;
using System.Collections;
using Blurift;

namespace Maniac
{
    public class GameLogic : WorldEventListener
    {
        protected bool isPlaying = false;

        public virtual void Start()
        {
            isPlaying = true;
        }

        public virtual void Update(CharacterController player)
        {

        }

        public virtual void PushEvent(object sender, string type, WorldEvent e)
        {
            
        }
    }

    public class GameLogicConquer : GameLogic
    {
        public override void Start()
        {
            base.Start();
            AIManager.Instance.SetSpawnersInvincible(false);
            WorldEventManager.Instance.AddListener("PlayerDied", this);
            WorldEventManager.Instance.AddListener("AISpawnerDied", this);
        }

        public override void Update(CharacterController player)
        {
            if (!isPlaying) return;
        }

        public override void PushEvent(object sender, string type, WorldEvent e)
        {
            switch (type)
            {
                case "PlayerDied":
                    GameEndScreen.Show();
                    GameEndScreen.SetMessage("You have died in the apocalypse.");
                    break;
                case "AISpawnerDied":
                    if (AIManager.Instance.SpawnersRemaining() == 0)
                    {
                        GameEndScreen.Show();
                        GameEndScreen.SetMessage("You have saved everyone from maniacs.");
                    }
                    break;
                case "AIDied":
                    break;
            }
        }
    }

    public class GameLogicWaves : GameLogic
    {
        private float startTime;
        private int score = 0;
        private int wavesCompleted = 0;

        private int enemiesPerWave = 10;
        private int enemiesAlive = 0;

        public override void Start()
        {
            base.Start();
            AIManager.Instance.SetSpawnersInvincible(true);
            AIManager.Instance.ContinousSpawning = false;
            WorldEventManager.Instance.AddListener("PlayerDied", this);
            WorldEventManager.Instance.AddListener("AIDied", this);
            StartWave();

            startTime = Time.time;
        }

        public override void Update(CharacterController player)
        {
            if (!isPlaying) return;
        }

        public override void PushEvent(object sender, string type, WorldEvent e)
        {
            switch (type)
            {
                case "PlayerDied":
                    GameEndScreen.Show();
                    float endTime = Time.time - startTime;
                    AIManager.Instance.Disable();
                    GameEndScreen.SetMessage("You have died in the apocalypse\nYou survived " + wavesCompleted + " waves over " + endTime + " seconds.\n You scored " + score + ".");
                    break;
                case "AIDied":
                    enemiesAlive--;
                    score += 10;
                    if (enemiesAlive == 0)
                    {
                        wavesCompleted++;
                        StartWave();
                    }

                    HUDManager.Instance.SetScore(score);
                    
                    break;
            }
        }

        private void StartWave()
        {
            //yield return new WaitForSeconds(2);
            
            AIManager.Instance.SpawnAI(enemiesPerWave);
            enemiesAlive = enemiesPerWave;
            enemiesPerWave *= (int)1.2f;
        }
    }
}