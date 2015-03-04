﻿/* Name: Test
 * Desc: Used to test other game objects without needing to go through the whole game
 * Author: Keirron Stach
 * Version: 0.6
 * Created: 1/04/2014
 * Edited: 20/04/2014
 */ 

using UnityEngine;
using System.Collections;

namespace Maniac
{
    [AddComponentMenu("EffectSystem/BehaviourAll")]
    public class EffectBehaviour : MonoBehaviour
    {

        public float FadeTime;
        public float LightPosition = 1;

        private float startTime;
        private float startIntensity;

        public string SortLayer = "";

        public bool StopEmmitingOnNoParent = false;

        //private Light light;
        //private ParticleSystem particleSystem;

        // Use this for initialization                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
        void Start()
        {
            startTime = Time.time;

            if (GetComponent<Light>() != null)
            {
                startIntensity = GetComponent<Light>().intensity;
                transform.position = new Vector3(transform.position.x, transform.position.y, LightPosition * -1);
            }

            if (GetComponent<Renderer>() != null)
                GetComponent<Renderer>().sortingLayerName = SortLayer;


        }

        // Update is called once per frame
        void Update()
        {
            bool stop = true;

            if (GetComponent<ParticleSystem>() != null)
            {
                if (StopEmmitingOnNoParent)
                {
                    if (transform.parent == null)
                    {
                        //particleSystem.enableEmission = false;
                        GetComponent<ParticleSystem>().Stop();
                    }
                }
                if (GetComponent<ParticleSystem>().isPlaying)
                    stop = false;
            }

            if (GetComponent<ParticleEmitter>() != null)
            {
                stop = false;
            }

            if (GetComponent<Light>() != null)
            {
                GetComponent<Light>().intensity = startIntensity * ((FadeTime - (Time.time - startTime)) / FadeTime);
                if (GetComponent<Light>().intensity < 0)
                    GetComponent<Light>().intensity = 0;

                if (Time.time - startTime <= FadeTime)
                {
                    stop = false;
                }
            }

            if (stop)
            {
                Destroy(gameObject);
            }
        }
    }
}
