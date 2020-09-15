using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum AnimState
{

}

public class AIMovement : MonoBehaviour
{
    public Transform target;
    public Rubbish carriedItem;
    public Transform trashCan;

    public bool chasing;
    public float speed = 1.0f;
    public bool allowMovement = true;

    public float chaseSpeed;
    public float walkSpeed;
    public float idleSpeed;


    public float chaseTimer = 0.0f;

    #region Setup
    private GlobalInfo globalInfo;
    Seeker seeker;
    Rigidbody2D rb;
    Animator animator;

    float nextWaypointDistance = 0.13f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEnd = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        globalInfo = CallbackHandler.instance.globalInfo;

        InvokeRepeating("UpdatePath", 0.0f, 0.5f);
    }
    #endregion

    public void ChangeAnimState()
    {
        animator.SetFloat("MoveSpeed", speed);
    }

    public void AllowMovement()
    {
        allowMovement = true;
    }

    public void FixedUpdate()
    {
        // No Path
        if (path == null)
        {
            speed = idleSpeed;
            ChangeAnimState();
            return;
        }
        // No Target
        if (!target)
        {
            speed = idleSpeed;
            ChangeAnimState();
            target = GetTarget();
            currentWaypoint = 0;
            UpdatePath();
            return;
        }

        speed = walkSpeed;
        ChangeAnimState();

        // In Chase 
        if (chasing)
        {
            speed = chaseSpeed;
            ChangeAnimState();
            chaseTimer -= Time.fixedDeltaTime;
            // End Chase
            if (chaseTimer <= 0)
            {
                chasing = false;
                target = GetTarget();
                currentWaypoint = 0;
                UpdatePath();
            }
            else if (target.GetComponent<PlayerMovement>())
            {
                if (target.GetComponent<PlayerMovement>().hidden)
                    return;
            }
        }

        // Check if Reached End, else Move
        reachedEnd = currentWaypoint >= path.vectorPath.Count;
        if (reachedEnd)
        {
            if (chasing)
            {
                PlayerMovement player = target.GetComponent<PlayerMovement>();
                // Swap items with Panda
                player.carriedItem.DropMe();
                carriedItem = player.carriedItem;
                player.carriedItem = null;
                carriedItem.CarryMe(this.transform);

                // GoTo Trashcan
                target = trashCan;
                currentWaypoint = 0;
                UpdatePath();

                // End Chase
                chasing = false;
                return;
            }
            else
            {
                // Picked up Rubbish
                if (target.GetComponent<Rubbish>())
                {
                    carriedItem = target.GetComponent<Rubbish>();
                    target.GetComponent<Rubbish>().CarryMe(this.transform);
                    target = trashCan;
                }
                // At Bin
                else if (target == trashCan && carriedItem)
                {
                    trashCan.GetComponent<Trashcan>().AddRubbish(carriedItem);
                    carriedItem = null;
                    target = GetTarget();
                }

                reachedEnd = false;
                currentWaypoint = 0;
                UpdatePath();
                return;
            }
        }

        if (target.GetComponent<Rubbish>())
        {
            if (target.GetComponent<Rubbish>().hidden)
            {
                target = null;
                return;
            }
        }

        if (allowMovement)
        {
            // Move
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 newPos = rb.position + direction * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
            // Next Waypoint
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (currentWaypoint >= path.vectorPath.Count - 2)
            {
                if (distance < nextWaypointDistance * 4)
                {
                    currentWaypoint++;
                }
            }
            else if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }
    }

    public Transform GetTarget()
    {
        float dist = Mathf.Infinity;
        Transform temp = null;

        foreach (Rubbish n in globalInfo.rubbishList)
        {
            if (!n.carried && !n.hidden)
            {
                if (Vector2.Distance(n.transform.position, transform.position) < dist)
                {
                    temp = n.transform;
                    dist = Vector2.Distance(n.transform.position, transform.position);
                }
            }
        }

        return (temp);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (target)
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        // If Player is in Radius
        if (player)
        {
            // And carrying an item
            if (player.carriedItem && !player.hidden)
            {
                // Drop Current Item
                if (carriedItem)
                {
                    carriedItem.DropMe();
                    carriedItem = null;
                }
                // Chase TrashPanda
                target = player.transform;
                chasing = true;
                chaseTimer = 3.0f;
            }
            if (target == player.transform && !player.carriedItem)
            {
                target = GetTarget();
                chasing = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        // If Player is in Radius
        if (player)
        {
            chaseTimer = 3.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        // If Player is in Radius
        if (player)
        {
            if (player.carriedItem)
            {
                if (!chasing)
                {
                    animator.SetTrigger("Alert");
                    allowMovement = false;
                    speed = idleSpeed;
                }

                chaseTimer = 3.0f;
                chasing = true;
            }
        }
    }
}
