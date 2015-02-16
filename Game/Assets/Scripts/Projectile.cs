using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public float speed;
	private Vector2 direction;
	private Vector2 testVec;
	void Start ()
	{
		//transform.LookAt (Input.mousePosition);
		direction = new Vector2 (Input.mousePosition.x - transform.position.x, Input.mousePosition.y - transform.position.y);
		direction.Normalize();
		Debug.Log (direction);
	}
	void Update ()
	{
		float amtToMove = speed * Time.deltaTime;
		//rigidbody2D.velocity = transform.forward * amtToMove;
		transform.Translate(direction * amtToMove);
	}
}
