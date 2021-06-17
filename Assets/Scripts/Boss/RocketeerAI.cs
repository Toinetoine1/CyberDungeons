using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Game;
using Pathfinding;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


public class RocketeerAI : MonoBehaviourPunCallbacks
{
    protected GameObject target, pl1, pl2;

    public float speed = 2f;
    public float nextWaypointDistance = 1f;
    public float lineOfSite = 5;
    public float distToShot = 5;

    private Path path;
    private int currentWaypoint;

    private Rigidbody2D rb;

    protected Animator Animator;

    private EnnemyWeapon ennemyWeapon;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        ennemyWeapon = GetComponent<EnnemyWeapon>();

        StartCoroutine(ExecuteAfterTime(0.5f));
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    protected void SetMovementAnim(Vector2 dir)
    {
        Animator.SetBool("Standing", false);
        Animator.SetFloat("xDir", dir.x);
        Animator.SetFloat("yDir", dir.y);
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
    


    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateTarget(o =>
        {
            if (path == null)
                return;
            Debug.Log("Changing target");
        });

        if (path == null)
            return;
        if (target == null)
            return;

        ennemyWeapon.target = target.transform;
    }

    protected void UpdateTarget(Action<GameObject> callback)
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
            }
            else
            {
                target = pl2;
            }
        }

        if (oldTarget != target)
        {
            callback.Invoke(target);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, distToShot);
    }
}