using System;
using System.Collections;
using Pathfinding;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace AI
{
    public class EnemyAI : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PlayerConnect playerConnect;

        private GameObject target, pl1, pl2;

        public float speed = 2f;
        public float nextWaypointDistance = 1f;
        public float lineOfSite = 5;

        private Path path;
        private int currentWaypoint;

        private Seeker seeker;
        private Rigidbody2D rb;
        
        private Animator Animator;

        // Start is called before the first frame update
        void Start()
        {
            seeker = GetComponent<Seeker>();
            rb = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();

            StartCoroutine(ExecuteAfterTime(0.5f));
            InvokeRepeating("UpdatePath", 0f, .5f);
        }
        
        private void SetMovementAnim(Vector2 dir)
        {
            Animator.SetBool("Standing", false);
            Animator.SetFloat("xDir", dir.x);
            Animator.SetFloat("yDir", dir.y);
        }
        
        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            Debug.Log("okkkk");
            foreach (Player pl in PhotonNetwork.CurrentRoom.Players.Values)
            {
                GameObject obj = GameObject.Find(pl.NickName);
                if (pl1 == null)
                    pl1 = obj;
                else
                    pl2 = obj;
            }
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
            UpdateTarget(o =>
            {
                if (path == null)
                    return;
                Debug.Log("Changing target");
                    seeker.CancelCurrentPathRequest();
                seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
            });
            if (path == null)
                return;
            if (target == null)
                return;

            if (Vector2.Distance(transform.position, target.transform.position) <= lineOfSite)
            {
                //TODO Fire a bullet
                Animator.SetBool("Standing", true);
                return;
            }
            
            Vector2 nextPos = Vector2.MoveTowards(transform.position, path.vectorPath[currentWaypoint],
                speed * Time.deltaTime);
            SetMovementAnim(nextPos);
            transform.position = nextPos;
            
            
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }

        private void UpdateTarget(Action<GameObject> callback)
        {
            GameObject oldTarget = target;
            
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
                    Debug.Log("PL1");
                }
                else
                {
                    target = pl2;
                    Debug.Log("PL2");
                }
            }

            if (oldTarget != target)
            {
                callback.Invoke(target);
                Debug.Log("ok ???");
            }
        }

        private void OnDrawGizmosSelected()
        {
            // Gizmos.color = Color.green;
            // Gizmos.DrawWireSphere(transform.position, lineOfSite);
        }
    }

}