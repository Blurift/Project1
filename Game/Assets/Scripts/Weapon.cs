using UnityEngine;
using System.Collections;

namespace Maniac
{
    public class Weapon : MonoBehaviour
    {
        public int clipSize;
		public int damage;
        private int currentAmmo;
        public AudioClip fireSound;
        private float nextFire;
		private float reserveAmmo;
        public float fireRate;
        public GameObject MuzzleFlashPrefab;
		private GameManager gameManager;
        // Use this for initialization
        void Start()
        {
            currentAmmo = clipSize;
			gameManager = GameObject.Find ("Managers").GetComponent<GameManager> ();
			UpdateAmmo ();

        }
        public void Fire()
        {
            if (Time.time > nextFire)
            {
                currentAmmo -= 1;
				reserveAmmo = clipSize - currentAmmo;
				UpdateAmmo ();
                SoundManager.Instance.Play(fireSound, false, transform.position);
                nextFire = Time.time + fireRate;
                ProjectileManager.Instance.CreateProj(this.transform, damage);

                if (MuzzleFlashPrefab != null)
                {
                    Instantiate(MuzzleFlashPrefab, transform.position, Quaternion.Euler(transform.up));
                }
            }
        }
        public bool CheckForReload()
        {
            if (currentAmmo == 0)
            {
                return false;
            }
            return true;
        }
		public void UpdateAmmo()
		{
			gameManager.SetAmmo(currentAmmo,clipSize);
		}
		public float GetMaxAmmo()
		{
			return clipSize;
		}

		public float GetAmmo()
		{
			return currentAmmo;
		}

		public float GetReserveAmmo()
		{
			return reserveAmmo;
		}

        public void Reload()
        {
            currentAmmo = clipSize;
			UpdateAmmo ();
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
