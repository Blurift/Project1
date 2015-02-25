using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public float speed;
	public float damage;

	void Start ()
	{
		rigidbody2D.AddForce (transform.up * speed);
	}
	void Update ()
	{

	}
	public void UpdateSpeed()
	{

		rigidbody2D.AddForce (transform.up * speed);
	}
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			return;
		}
		else if (other.gameObject.GetComponent<Health> () != null)
		{
			other.gameObject.GetComponent<Health> ().TakeDamage (damage);

		} 
		ProjectileManager.Instance.ProjHit (this);
		//Destroy(gameObject);
	}
		


		

}



