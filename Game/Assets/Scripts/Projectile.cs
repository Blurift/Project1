using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public float speed;
	public float damage;

	void Start ()
	{
	}
	void Update ()
	{
		float amtToMove = speed * Time.deltaTime;
		rigidbody2D.velocity =  transform.up * amtToMove;
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
		Destroy(gameObject);
	}
		


		

}



