using UnityEngine;
using System.Collections;
using Blurift;
using Pathfinding;

namespace Maniac
{
    [RequireComponent(typeof(MovementController))]
    public class AI : MonoBehaviour
    {

        private GameObject target;
        public int scoreValue;
        private MovementController movement;

        //Pathfinding
        private Seeker seeker;
        private float waypointPadding = 2;
        private int currentWaypoint = 0;
        private Vector3 targetPosition;
        private Path path;
        private float nextPathReset = 0;

        // Use this for initialization
        void Start()
        {
            movement = GetComponent<MovementController>();
            seeker = GetComponent<Seeker>();
            seeker.pathCallback += OnPathComplete;
        }

        public void Initialize()
        {
            

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            float distance = float.MaxValue;

            //Debug.Log("Found " + players.Length + " players");

            foreach (GameObject go in players)
            {
                float d = Vector2.Distance(transform.position, go.transform.position);
                if (d < distance)
                {
                    distance = d;
                    target = go;
                }
            }

            
        }

        // Update is called once per frame
        void Update()
        {
            if (gameObject.GetComponent<Health>().isAlive == Health.State.Dead)
            {
                AIManager.Instance.AIDied(this);

            }

            if (target == null)
                return;

            float distance = Vector2.Distance(transform.position, target.transform.position);

            

            if(distance < 3)
            {
                movement.MoveForward();
                movement.RotateTowards(target.transform.position);
                return;
            }

            if (Time.time > nextPathReset)
            {
                seeker.StartPath(transform.position, target.transform.position);
                nextPathReset = Time.time + 4;
            }

            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                seeker.StartPath(transform.position, target.transform.position);
                nextPathReset = Time.time + 4;
                return;
            }

            movement.MoveForward();
            movement.RotateTowards(path.vectorPath[currentWaypoint]);

            if (Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]) < waypointPadding)
                currentWaypoint++;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                AIManager.Instance.AIHitTarget(col.gameObject, this);
            }
        }

        public void OnPathComplete(Path p)
        {
            if(!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        }
    }
}
