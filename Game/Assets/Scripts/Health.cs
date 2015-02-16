using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{

	public float health;
	public float maxHealth;
	// Use this for initialization
	void Start () {
	
	}
	public void takeDamage(float damage)
	{
		health -= damage;
	}
	// Update is called once per frame
	void Update () 
	{
		if (health > maxHealth) 
		{
			health = maxHealth;
		}

		if (health <= 0)
		{
			Destroy(gameObject);
		}
	
	}
}
