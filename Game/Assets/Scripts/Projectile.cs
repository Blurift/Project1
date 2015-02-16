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

		if (other.gameObject.layer == 8) 
		{
			if (other.gameObject.GetComponent<Health>()!= null)
			{
				other.gameObject.GetComponent<Health>().takeDamage(damage);
			}

			Destroy(gameObject);
		}

		if (other.gameObject.layer == 9)
		{
			Destroy(gameObject);
		}

	}

}

