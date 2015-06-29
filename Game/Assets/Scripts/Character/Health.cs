using UnityEngine;
using System.Collections;
using Blurift;

namespace Maniac
{
    [RequireComponent(typeof(EntityEventManager))]
    public class Health : MonoBehaviour
    {
        public enum State { Alive, Dead };

        private float health;
        public float HealthCurrent
        {
            get { return health; }
        }

        public float maxHealth;
        public State isAlive;
        public string EffectDecal = "";
        // Use this for initialization

        public GameObject OnHitEffect;

        private EntityEventManager events;

        void Start()
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

            if (OnHitEffect != null)
                Instantiate(OnHitEffect, transform.position, Quaternion.identity);

            if (EffectDecal != "")
                EffectManager.Instance.CreateEffect(EffectDecal, transform.position);
        }
        // Update is called once per frame
        void Update()
        {
            if (health > maxHealth)
            {
                health = maxHealth;
            }

            if (health <= 0)
            {
                if (isAlive == State.Alive)
                {
                    events.PushEvent(this, "HealthDead", new EntityEvent(transform.position));
                    isAlive = State.Dead;
                }
            }

        }
    }
}
