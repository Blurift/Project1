using UnityEngine;
using System.Collections;
using Blurift;

[RequireComponent(typeof(EntityEventManager))]
public class Health : MonoBehaviour 
{
	public enum State{Alive, Dead};
	private float health;
	public float maxHealth;
	public State isAlive;
	// Use this for initialization

    public GameObject OnHitEffect;

    private EntityEventManager events;

	void Start () 
	{
		isAlive = State.Alive;
		health = maxHealth;

        events = GetComponent<EntityEventManager>();
	}
	public void ResetHealth()
	{
		health = maxHealth;
		isAlive = State.Alive;
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

        events.PushEvent(this, "HealthDamage", new EntityEvent(transform.position));

        if(OnHitEffect != null)
            Instantiate(OnHitEffect, transform.position, Quaternion.identity);
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
