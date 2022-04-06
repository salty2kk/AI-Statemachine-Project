using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    #region Variables
    [Tooltip("The AI Agents movement speeds.")]
    [SerializeField] public float speed = 3f;
    [SerializeField] public Transform player;                                               // this transform is assigned in the inspector so the AI knows what to chase (in this case the player)
    [SerializeField] public float chaseDistance = 4;                                        // this is the distance the AI will recognize the player and chase

    [Tooltip("The waypoints that the AI follows by default.")]
    public List<Transform> patrolGoals;                                                     // note - lists are good if you are changing the size of the array  

    public int goalIndex = 0;                                                               // this is the number used to specify which Transform in the List is specified
    public float minGoalDistance = 0.05f;                                                   // this number is used to check if our position is really close to the waypoint

    #endregion
    
    public void AIMoveTowards(Transform goal)                                               // this function requires a transform for it to be called is referred to as "goal"
    {
        Vector2 AIPosition = transform.position;

        if (Vector2.Distance(AIPosition, goal.position) > minGoalDistance)                  // if the distance between our position and the goals is greater than 0.05
        {
                                                                                            //                                          (A)          (B)                      
            Vector2 directionToGoal = (goal.position - transform.position);                 // this gets the direction from it's own position to the goal by subtracting  (B - A)
                  
            directionToGoal.Normalize();                                                    // makes this vector have a magnitude of 1
            transform.position += (Vector3)directionToGoal * speed * Time.deltaTime;
        }
    }

    public void WaypointUpdate()
    {
        Vector2 AIPosition = transform.position;

        if (Vector2.Distance(AIPosition, patrolGoals[goalIndex].position) < minGoalDistance) // if the distance between our position and the waypoints position is less than 0.05
        {
            goalIndex++;                                                                     // increase the value of goalIndex up by 1

            if (goalIndex >= patrolGoals.Count)                                              // if the index number is greater than the amount in the List
            {
                goalIndex = 0;                                                               // set the index to 0 and go back to first waypoint
            }
        }
    }

}
