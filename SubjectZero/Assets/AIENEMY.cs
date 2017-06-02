using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class AIENEMY : MonoBehaviour {

    //A quien persigue?
    public Transform target;

    //How many times wach secod we will update our path
    public float updateDRate = 2f;

    private Seeker seeker;
    private Rigidbody2D rb;

    //Calculated path
    public Path path;

    //AI's speed per second
    public float speed = 5f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;

    //Max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;

    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if(target == null)
        {
            Debug.LogError("Player no encontrado");
            return;
        }

        //Start a new path to the target position, return the result to the OnPathComplete method
        seeker.StartPath (transform.position, target.position, OnPathComplete);

        StartCoroutine (UpdatePath ());
    }

    IEnumerator UpdatePath() {    
        if (target == null) {
           //TODO: Insert a player search here.
           yield return false;
        }

        //Start a new path to the target position, return the result to the OnPathComplete method
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds ( 1f / updateDRate);
        StartCoroutine (UpdatePath());
    }

    public void OnPathComplete (Path p)
    {
        Debug.Log("Camino encontrado, ¿hay algun error?" + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (target == null) {
            return false;
        }

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count) {
            if (pathIsEnded)
                return;

            Debug.Log("End of path reached");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        //Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //Move the AI
        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
}
