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

        public int PoolSize = 10;
        public GameObject[] AIPrefabs;
        private Dictionary<GameObject, AI[]> aiPool = new Dictionary<GameObject, AI[]>();


        public float SpawnFrequency = 2;
        private float nextSpawn = 0;

        private List<AISpawner> spawners = new List<AISpawner>();
        private GameManager gameManager;

        public bool ContinousSpawning = true;
        private int aiToSpawn = 0;
        private bool spawnerInvincible = true;

        // Use this for initialization
        void Start()
        {
            isRunning = true;

            gameManager = GetComponent<GameManager>();
            if (AIPrefabs.Length > 0)
            {
                for (int i = 0; i < AIPrefabs.Length; i++)
                {
                    AI ai = AIPrefabs[i].GetComponent<AI>();
                    aiPool.Add(AIPrefabs[i], new AI[ai.MaxInGame]);
                    for (int j = 0; j < ai.MaxInGame; j++)
                    {
                        aiPool[AIPrefabs[i]][j] = ((GameObject)Instantiate(AIPrefabs[i % AIPrefabs.Length])).GetComponent<AI>();
                        aiPool[AIPrefabs[i]][j].gameObject.SetActive(false);
                    }
                    
                }
            }

            GameObject[] spawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
            for (int i = 0; i < spawns.Length; i++)
            {
                AISpawner spawner = spawns[i].GetComponent<AISpawner>();
                spawner.Invincible = spawnerInvincible;
                spawners.Add(spawner);
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
                    AISpawner spawner = spawners[rIndex];
                    GameObject aiPrefab = GetToSpawn(spawner);

                    AI selectedAI = null;

                    for (int i = 0; i < aiPool[aiPrefab].Length; i++)
                    {
                        if (!aiPool[aiPrefab][i].gameObject.activeSelf)
                            selectedAI = aiPool[aiPrefab][i];
                    }

                    if (selectedAI != null)
                    {
                        selectedAI.transform.position = spawner.transform.position;
                        selectedAI.gameObject.SetActive(true);
                        selectedAI.gameObject.GetComponent<Health>().ResetHealth();
                        selectedAI.Initialize();
                    }

                    if (!ContinousSpawning)
                        aiToSpawn--;
                }


                nextSpawn = Time.time + SpawnFrequency;
            }
        }

        private GameObject GetToSpawn(AISpawner spawner)
        {
            int index = Random.Range(0, spawner.EnemiesToSpawn.Length);

            return spawner.EnemiesToSpawn[index];
        }

        public void SpawnAI(int amount)
        {
            aiToSpawn = amount;
        }

        public void Disable()
        {
            isRunning = false;
            for (int i = 0; i < AIPrefabs.Length; i++)
            {
                AI[] aiArray = aiPool[AIPrefabs[i]];
                for (int j = 0; j < aiArray.Length; j++)
                {
                    aiArray[j].gameObject.SetActive(false);
                }
            }
            this.enabled = false;
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
            spawnerInvincible = state;
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
                h.TakeDamage(ai.MeleeDamage);
            }
        }
    }
}
