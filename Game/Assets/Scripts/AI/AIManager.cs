using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Blurift;

namespace Maniac
{
    public class AIManager : MonoBehaviour
    {

        private static AIManager _instance;
        public static AIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<AIManager>();
                }
                return _instance;
            }
        }

        private bool isRunning = false;

        private AI[] aiPool;
        public int PoolSize = 10;
        public GameObject AIPrefab;

        public float SpawnFrequency = 2;
        private float nextSpawn = 0;

        private List<AISpawner> spawners = new List<AISpawner>();
        private GameManager gameManager;

        public bool ContinousSpawning = true;
        private int aiToSpawn = 0;

        // Use this for initialization
        void Start()
        {
            isRunning = true;

            gameManager = GetComponent<GameManager>();
            if (AIPrefab != null)
            {
                aiPool = new AI[PoolSize];
                for (int i = 0; i < PoolSize; i++)
                {
                    aiPool[i] = ((GameObject)Instantiate(AIPrefab)).GetComponent<AI>();
                    aiPool[i].gameObject.SetActive(false);
                }
            }

            GameObject[] spawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
            for (int i = 0; i < spawns.Length; i++)
            {
                spawners.Add(spawns[i].GetComponent<AISpawner>());
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!isRunning) return;

            if (Time.time > nextSpawn && (ContinousSpawning || aiToSpawn > 0))
            {
                if (spawners.Count > 0)
                {
                    //Get the index of the spawn to spawn the enemy at.
                    int rIndex = Random.Range(0, spawners.Count);
                    int index = -1;

                    for (int i = 0; i < PoolSize; i++)
                    {
                        if (!aiPool[i].gameObject.activeSelf)
                            index = i;
                    }

                    if (index != -1)
                    {
                        aiPool[index].transform.position = spawners[rIndex].transform.position;
                        aiPool[index].gameObject.SetActive(true);
                        aiPool[index].gameObject.GetComponent<Health>().ResetHealth();
                        aiPool[index].Initialize();
                    }

                    if (!ContinousSpawning)
                        aiToSpawn--;
                }


                nextSpawn = Time.time + SpawnFrequency;
            }
        }

        public void SpawnAI(int amount)
        {
            aiToSpawn = amount;
        }

        public void Disable()
        {
            isRunning = false;
            for (int i = 0; i < PoolSize; i++)
            {
                aiPool[i].gameObject.SetActive(false);
                this.enabled = false;
            }
        }

        public void AIDied(AI ai)
        {
            ai.gameObject.SetActive(false);
            WorldEventManager.Instance.PushEvent(this, "AIDied", new WorldEvent(ai.transform.position));
        }

        #region Spawner Methods

        public void SpawnerDied(AISpawner spawner)
        {
            spawners.Remove(spawner);
            WorldEventManager.Instance.PushEvent(this, "AISpawnerDied", new WorldEvent(spawner.transform.position));
        }

        public void SetSpawnersInvincible(bool state)
        {
            for (int i = 0; i < spawners.Count; i++)
            {
                spawners[i].Invincible = state;
            }
        }

        public int SpawnersRemaining()
        {
            return spawners.Count;
        }


        #endregion

        public void AIHitTarget(GameObject target, AI ai)
        {
            Health h = target.GetComponent<Health>();

            if (h != null)
            {
                //Debug.Log("Hit");
                h.TakeDamage(ai.MeleeDamage);
            }
        }
    }
}
