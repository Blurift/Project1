using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public float speed;
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
		if (!(other.tag == "Player")) 
		{
			if (other.gameObject.tag == "Enemy") 
			{
				Destroy (other.gameObject);
				Destroy(gameObject);
			}
			if (other.gameObject.tag == "Border")
			{
				Destroy(gameObject);
			}


		}

	}
}
