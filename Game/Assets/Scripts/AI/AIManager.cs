using UnityEngine;
using System.Collections;

public class AIManager : MonoBehaviour {

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

    private AI[] aiPool;
    public int PoolSize = 10;
    public GameObject AIPrefab;

    public float SpawnFrequency = 2;
    private float nextSpawn = 0;

    private GameObject[] spawns;
	private GameManager gameController;

	// Use this for initialization
	void Start ()
    {
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent <GameManager>();
		}
		if (gameController == null) 
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	    if(AIPrefab != null)
        {
            aiPool = new AI[PoolSize];
            for (int i = 0; i < PoolSize; i++)
            {
                aiPool[i] = ((GameObject)Instantiate(AIPrefab)).GetComponent<AI>();
                aiPool[i].gameObject.SetActive(false);
            }
        }

        spawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time > nextSpawn)
        {
            if(spawns.Length > 0)
            {
                //Get the index of the spawn to spawn the enemy at.
                int rIndex = Random.Range(0, spawns.Length);
                int index = -1;

                for (int i = 0; i < PoolSize; i++)
                {
                    if (!aiPool[i].gameObject.activeSelf)
                        index = i;
                }

                if(index != -1)
                {
                    aiPool[index].transform.position = spawns[rIndex].transform.position;
                    aiPool[index].gameObject.SetActive(true);
					aiPool[index].gameObject.GetComponent<Health>().ResetHealth();
                    aiPool[index].Initialize();
                }
            }


            nextSpawn = Time.time + SpawnFrequency;
        }
	}

    public void Disable()
    {
        for (int i = 0; i < PoolSize; i++)
        {
            aiPool[i].gameObject.SetActive(false);
            this.enabled = false;
        }
    }

	public void AIDying(AI ai)
	{
		gameController.AddScore (ai.scoreValue);
		ai.gameObject.SetActive(false);
	}

    public void AIHitTarget(GameObject target, AI ai)
    {
        Health h = target.GetComponent<Health>();

        if(h != null)
        {
            h.TakeDamage(5);
            ai.gameObject.SetActive(false);
        }

        //TODO Make some sort of effect here.
    }
}
