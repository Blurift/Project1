using UnityEngine;
using System.Collections;

namespace Maniac
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementController : MonoBehaviour
    {

        public float MoveForce;
        public float MaxRotation;
        public Rigidbody2D body;

        public AudioClip[] FootstepSounds;
        private float nextFootstep = 0;
        public float FootstepFreq = 0.5f;

        private Animator animator;

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            body = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            bool moving = body.velocity != Vector2.zero;
            if (animator != null)
            {
                animator.SetBool("Moving", moving);
            }

            if (moving)
            {
                if(Time.time > nextFootstep && FootstepSounds.Length > 0)
                {
                int rfi = Random.Range(0, FootstepSounds.Length); //Random Footstep Index

                SoundManager.Instance.Play(FootstepSounds[rfi], false, transform.position);

                nextFootstep = Time.time + FootstepFreq;
                }

            }
        }

        public void MoveForward()
        {
            MoveVertical(1);
        }

        public void MoveBackward()
        {
            MoveVertical(-1);
        }

        public void MoveVertical(float power)
        {
            if (power > 1) power = 1;
            if (power < -1) power = -1;
            Move(new Vector2(0, power));
        }

        public void MoveRight()
        {
            MoveHorizontal(1);
        }

        public void MoveLeft()
        {
            MoveHorizontal(-1);
        }

        public void MoveHorizontal(float power)
        {
            if (power > 1) power = 1;
            if (power < -1) power = -1;
            Move(new Vector2(power, 0));
        }

        public void Move(Vector2 force)
        {
            force.Normalize();
            Vector2 right = transform.right * force.x;
            Vector2 up = transform.up * force.y;

            Vector2 applied = (right + up).normalized;

            body.AddForce(applied * MoveForce);
        }





        public void RotateTowards(Vector2 pos)
        {
            Vector2 rel = pos - (Vector2)transform.position;
            float angle = Mathf.Atan2(-rel.x, rel.y) * Mathf.Rad2Deg;

            RotateTowards(angle);
        }

        public void RotateTowards(float angle)
        {
            float current = transform.rotation.eulerAngles.z;
            float max = MaxRotation * Time.deltaTime;

            float difference = angle - current;

            if (difference < -180)
                difference += 360;
            if (difference > 180)
                difference -= 360;

            if (difference < 0)
            {
                max *= -1;
                if (difference < max)
                    difference = max;
            }
            else
            {
                if (difference > max)
                    difference = max;
            }

            transform.Rotate(Vector3.forward, difference);
        }
    }
}
