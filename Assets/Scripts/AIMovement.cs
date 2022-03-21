using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    #region Variables
    [Tooltip("The AI Agents movement speeds.")]
    [SerializeField] public float speed = 2f;
    [SerializeField] public float chaseSpeed = 3.5f;


    [SerializeField] public Transform player; // this transform is assigned in the inspector so the AI knows what to chase
    [SerializeField] public float chaseDistance = 3; // this is the distance the AI will recognize the player and chase

    [Tooltip("The waypoints that the AI follows by default.")]
    public List<Transform> patrolGoals;
    private int goalIndex = 0;
    public float minGoalDistance = 0.05f;

    #endregion

    public void Update()
    {
        if(Vector2.Distance(transform.position, player.position) < chaseDistance)
        {
            AIMoveTowards(player);
        }
        else
        {
            WaypointUpdate();
            AIMoveTowards(patrolGoals[goalIndex]);
        }
    }

    void AIMoveTowards(Transform goal)
    {
        Vector2 AIPosition = transform.position;

        if (Vector2.Distance(AIPosition, goal.position) > minGoalDistance)
        {
            //direction from A to B
            // is B - A
            //method 3
            Vector2 directionToGoal = (goal.position - transform.position);
            directionToGoal.Normalize();
            transform.position += (Vector3)directionToGoal * speed * Time.deltaTime;
        }
    }

    public void WaypointUpdate()
    {
        Vector2 AIPosition = transform.position;

        //if we are  near the goal
        if (Vector2.Distance(AIPosition, patrolGoals[goalIndex].position) < minGoalDistance)
        {
            //++ increment by 1
            //increase the value of waypointIndex up by 1
            goalIndex++;

            if (goalIndex >= patrolGoals.Count)
            {
                goalIndex = 0;
            }
        }
    }
    /*
    void Patrol()
    {
        float distance = Vector2.Distance(transform.position) patrolGoal.transform.position);

        if (distance > 0.05f)
        {
            
        }
    }
    */
}
