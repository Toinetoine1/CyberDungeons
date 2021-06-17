using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;
using Pathfinding;
using Photon.Pun;
using Photon.Realtime;


public class PotatorIA : EnemyAI
{

    private PotatorWeapon _potatorWeapon;
    
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _potatorWeapon = GetComponent<PotatorWeapon>();    

        StartCoroutine(ExecuteAfterTime(0.5f));
        InvokeRepeating("UpdatePath", 0f, .5f);
    }
    
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

            _potatorWeapon.target = target.transform;

            if (Vector2.Distance(transform.position, target.transform.position) <= distToShot &&
                !Physics2D.Linecast(transform.position, target.transform.position, 1 << LayerMask.NameToLayer("WallColider")))
            {
                Animator.SetBool("Standing", true);

                if (Vector2.Distance(transform.position, target.transform.position) <= lineOfSite)
                    return;
            }

            if (currentWaypoint >= 0 && currentWaypoint < path.vectorPath.Count)
            {
                Vector2 nextPos = Vector2.MoveTowards(transform.position, path.vectorPath[currentWaypoint],
                    speed * Time.deltaTime);
            
                SetMovementAnim((nextPos - (Vector2)transform.position).normalized);
                transform.position = nextPos;
                
                float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

                if (distance < nextWaypointDistance)
                {
                    currentWaypoint++;
                }
            }
    }
    
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        foreach (Player pl in PhotonNetwork.CurrentRoom.Players.Values)
        {
            GameObject obj = GameObject.Find(pl.NickName);
            if (pl1 == null)
                pl1 = obj;
            else
                pl2 = obj;
        }
    }
}
