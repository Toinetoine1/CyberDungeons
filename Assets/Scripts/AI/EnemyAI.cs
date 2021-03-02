using System;
using Pathfinding;
using UnityEngine;

namespace AI
{
    public class EnemyAI : MonoBehaviour
    {
        public Transform target;

        public float speed = 2f;
        public float nextWaypointDistance = 1f;
        public float lineOfSite = 5;

        private Path path;
        private int currentWaypoint;

        private Seeker seeker;
        private Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {
            seeker = GetComponent<Seeker>();
            rb = GetComponent<Rigidbody2D>();

            InvokeRepeating("UpdatePath", 0f, .5f);
        }

        private void UpdatePath()
        {
            if (seeker.IsDone() && target != null)
                seeker.StartPath(rb.position, target.position, OnPathComplete);
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (path == null)
                return;
            if (target == null)
                return;
            
            if (Vector2.Distance(transform.position, target.position) <= lineOfSite)
            {
                //TODO Fire a bullet
                return;
            }
            
            transform.position = Vector2.MoveTowards(transform.position, path.vectorPath[currentWaypoint],
                speed * Time.deltaTime);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }

        private void OnDrawGizmosSelected()
        {
            // Gizmos.color = Color.green;
            // Gizmos.DrawWireSphere(transform.position, lineOfSite);
        }
    }
}