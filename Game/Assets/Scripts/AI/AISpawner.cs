﻿using UnityEngine;
using System.Collections;
using Blurift;

namespace Maniac
{
    public class AISpawner : MonoBehaviour, EntityEventListener
    {
        private EntityEventManager events;
        public bool Invincible = true;
        public GameObject[] EnemiesToSpawn;

        // Use this for initialization
        void Start()
        {
            events = GetComponent<EntityEventManager>();

            events.AddListener("HealthDamage", this);
            events.AddListener("HealthDead", this);
        }

        public void PushEvent(object sender, string type, EntityEvent e)
        {
            switch (type)
            {
                case "HealthDamage":
                    break;
                case "HealthDead":
                    if(!Invincible)
                    {
                        AIManager.Instance.SpawnerDied(this);
                        FadeAllSprites();
                    }
                    break;
            }
        }

        private void FadeAllSprites()
        {
            foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                renderer.color = new Color(0.3f, 0.3f, 0.3f, 1.0f);
            }
        }
    }
}