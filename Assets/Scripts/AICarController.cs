using UnityEngine;

public class AICarController : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 10f;
    public float turnSpeed = 5f;
    public float waypointReachDistance = 5f;

    private int currentWaypoint = 0;

    void Update()
    {
        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        Transform target = waypoints[currentWaypoint];

        // Direction to waypoint
        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        // Rotate towards waypoint
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            turnSpeed * Time.deltaTime
        );

        // Move forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Check distance
        if (Vector3.Distance(transform.position, target.position) < waypointReachDistance)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0; // Loop race
        }
    }

    public void StopCar()
    {
        speed = 0;
        this.enabled = false;
    }
}
