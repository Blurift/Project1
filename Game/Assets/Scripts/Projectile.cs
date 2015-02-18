using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public float speed;
	public float damage;
	private Vector3 dir;
	void Start ()
	{
		Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
		dir = (Input.mousePosition - sp).normalized;
	}
	void Update ()
	{
		float amtToMove = speed * Time.deltaTime;
		rigidbody2D.velocity = dir * amtToMove;
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



