using System;
using Pathfinding;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace AI
{
    public class EnemyAI : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PlayerConnect playerConnect;

        private GameObject target;

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
                seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
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
            UpdateTarget();
            if (target == null)
                return;

            if (Vector2.Distance(transform.position, target.transform.position) <= lineOfSite)
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

        private void UpdateTarget()
        {
            GameObject pl1 = playerConnect.Player1;
            GameObject pl2 = playerConnect.Player2;

            if (pl1 != null && pl2 == null)
                target = pl1;
            else if (pl2 != null && pl1 == null)
                target = pl2;
            else if (pl1 != null && pl2 != null)
            {
                var position = transform.position;
                float distBetweenPl1AndEnemy = Vector2.Distance(position, pl1.transform.position);
                float distBetweenPl2AndEnemy = Vector2.Distance(position, pl2.transform.position);

                if (distBetweenPl2AndEnemy >= distBetweenPl1AndEnemy)
                {
                    target = pl1;
                }
                else
                {
                    target = pl2;
                }
            }

            if (target != null)
                Debug.Log("Targeting " + target.name);
        }

        private void OnDrawGizmosSelected()
        {
            // Gizmos.color = Color.green;
            // Gizmos.DrawWireSphere(transform.position, lineOfSite);
        }
    }
}