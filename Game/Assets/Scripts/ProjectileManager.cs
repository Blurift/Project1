using UnityEngine;
using System.Collections;

namespace Maniac
{
    public class ProjectileManager : MonoBehaviour
    {

        #region Singleton Code
        private static ProjectileManager _instance;
        public static ProjectileManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ProjectileManager>();
                }
                return _instance;
            }
        }
        #endregion

        private Projectile[] projPool;
        public int PoolSize = 10;
        public GameObject ShotPrefab;
        // Use this for initialization
        void Start()
        {
            if (ShotPrefab != null)
            {
                projPool = new Projectile[PoolSize];
                for (int i = 0; i < PoolSize; i++)
                {
                    projPool[i] = ((GameObject)Instantiate(ShotPrefab)).GetComponent<Projectile>();
                    projPool[i].gameObject.SetActive(false);
                }
            }

        }

        public void ProjHit(Projectile proj)
        {
            proj.gameObject.SetActive(false);

        }

        public void CreateProj(Transform spawnPoint)
        {
            int index = -1;
            for (int i = 0; i < projPool.Length; i++)
            {
                if (!projPool[i].gameObject.activeSelf)
                {
                    index = i;
                }
            }
            if (index != -1)
            {
                projPool[index].transform.position = spawnPoint.position;
                projPool[index].transform.rotation = spawnPoint.rotation;
                projPool[index].gameObject.SetActive(true);
                projPool[index].UpdateSpeed();

            }
            else
            {
                projPool[projPool.Length - 1] = ((GameObject)Instantiate(ShotPrefab, spawnPoint.position, spawnPoint.rotation)).GetComponent<Projectile>();
            }

        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
