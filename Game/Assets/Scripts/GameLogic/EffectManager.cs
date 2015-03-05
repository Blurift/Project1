using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Maniac
{
    public class EffectManager : MonoBehaviour
    {
        private static EffectManager _instance;
        public static EffectManager Instance
        {
            get
            {
                if(_instance == null)
                    _instance = FindObjectOfType<EffectManager>();
                return _instance;
            }
        }

        public Dictionary<string, EffectGroup> GroupDict = new Dictionary<string,EffectGroup>();
        public EffectGroup[] Groups;

        public GameObject effectPrefab;

        private Effect[] pool;

        void Start()
        {
            pool = new Effect[100];

            for (int i = 0; i < 100; i++)
            {
                pool[i] = new Effect();
                pool[i].GameObject = (GameObject)Instantiate(effectPrefab);
                pool[i].Renderer = pool[i].GameObject.GetComponent<SpriteRenderer>();
            }

            for (int i = 0; i < Groups.Length; i++)
            {
                GroupDict.Add(Groups[i].GroupName, Groups[i]);
            }
        }

        void Update()
        {
            for (int i = 0; i < pool.Length; i++)
            {
                Effect e = pool[i];
                if(e.GameObject.activeSelf)
                {
                    if (Time.time > e.Expire)
                        e.GameObject.SetActive(false);
                }
            }
        }

        public void CreateEffect(string effectType, Vector2 position)
        {
            Effect e = null;

            for (int i = 0; i < pool.Length; i++)
            {
                if(!pool[i].GameObject.activeSelf)
                {
                    e = pool[i];
                }
            }

            if (e == null) return;

            e.GameObject.transform.position = position;
            e.GameObject.SetActive(true);
            e.GameObject.transform.rotation = Quaternion.EulerAngles(0, 0, Random.Range(0f, 360f));
            e.Renderer.sprite = GroupDict[effectType].GetSprite();
            e.Expire = Time.time + 1000;
        }
        
        private class Effect
        {
            public GameObject GameObject;
            public SpriteRenderer Renderer;

            public float Expire = 0;
        }
    }

    [System.Serializable]
    public class EffectGroup
    {
        public string GroupName;
        public Sprite[] sprites;

        public Sprite GetSprite()
        {
            int index = Random.Range(0, sprites.Length);
            return sprites[index];
        }
    }
}