using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public enum State{Alive, Dead};
	private float health;
	public float maxHealth;
	public State isAlive;
	// Use this for initialization
	void Start () 
	{
		isAlive = State.Alive;
		health = maxHealth;
	
	}
	public void ResetHealth()
	{
		health = maxHealth;
	}
	public void SetState(State state)
	{
		isAlive = state;
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
