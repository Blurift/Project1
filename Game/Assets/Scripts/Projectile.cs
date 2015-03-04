using UnityEngine;
using System.Collections;

namespace Maniac
{
    public class Projectile : MonoBehaviour
    {
        public float speed;
        public float damage;

        public GameObject HitEffectPrefab;

        void Start()
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
        }

        void Update()
        {

        }

        public void UpdateSpeed()
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return;
            }
            else if (other.gameObject.GetComponent<Health>() != null)
            {
                other.gameObject.GetComponent<Health>().TakeDamage(damage);

            }
            ProjectileManager.Instance.ProjHit(this);

            if (HitEffectPrefab != null)
            {
                GameObject t = (GameObject)Instantiate(HitEffectPrefab, transform.position, Quaternion.identity);
                t.transform.localRotation = Quaternion.LookRotation(transform.up * -1);
            }

        }





    }
}



