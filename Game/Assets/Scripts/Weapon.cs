using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public int clipSize;
	private int currentAmmo;
	public AudioClip fireSound;
	private float nextFire;
	public float fireRate;

    public GameObject MuzzleFlashPrefab;
	// Use this for initialization
	void Start () 
	{
		currentAmmo = clipSize;
	
	}
	public void Fire()
	{
		if (Time.time > nextFire) 
		{
			currentAmmo -= 1;
			SoundManager.Instance.Play(fireSound, false, transform.position);
			nextFire = Time.time + fireRate;
			ProjectileManager.Instance.CreateProj(this.transform);

            if(MuzzleFlashPrefab != null)
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
	public void Reload()
	{
		currentAmmo = clipSize;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
