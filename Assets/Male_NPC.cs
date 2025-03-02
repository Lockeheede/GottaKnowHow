using UnityEngine;

public class Male_NPC : MonoBehaviour
{
    public Animator animator;
    public float walkSpeed = 2f;
    public float knockoutDuration = 5f;
    public Transform[] waypoints;

private int _currentWaypointIndex = 0;
private bool _isKnockedOut = false;

private float idleDuration = 2f;
private float idleTimer = 0f;

    private void Update()
    {
        if (_isKnockedOut) return;

        if(idleTimer > 0)
        {
            idleTimer -= Time.deltaTime;
            animator.SetTrigger("Idle");
            return;
        }

        //Move towards the current waypoint
        Transform targetWaypoint = waypoints[_currentWaypointIndex];
        Vector3 moveDirection = (targetWaypoint.position - transform.position).normalized;
        transform.position += moveDirection * walkSpeed * Time.deltaTime;

        //Rotate towards the waypoint
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 5f * Time.deltaTime);

        //Check if the NPC has reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            //Wait a variable number of seconds while idling at the waypoint
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;//Move to the next waypoint
            idleTimer = idleDuration;
        }

        //Trigger the walk animation
        animator.SetTrigger("Walk");
    }

    private void MoveToNextWaypoint()
    {
        //Trigger the idle animation
        animator.SetTrigger("Idle");
    }

    public void KnockOut()
    {
        if(_isKnockedOut) return;

        //Trigger knockedout animation
        animator.SetTrigger("Knockout");

        //disable movement
        _isKnockedOut = true;

        //After time duration, destroy the NPC
        Destroy(gameObject, knockoutDuration);
    }
}
