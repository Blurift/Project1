using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	private enum State{Alive, Dead};
	private float health;
	public float maxHealth;
	public State isAlive;
	// Use this for initialization
	void Start () 
	{
		isAlive = State.Alive;
		health = maxHealth;
	
	}
	public float GetHealth()
	{
		return health;
	}
	public void TakeDamage(float damage)
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
			isAlive = State.Dead;
		
		}
	
	}
}
