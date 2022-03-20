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

    [SerializeField] private GameObject[] patrolGoal;
    private int goalIndex;

    #endregion

    public void Update()
    {
        if(Vector2.Distance(transform.position, player.position) < chaseDistance)
        {
            ChaseThePlayer(player);
        }
    }

    void ChaseThePlayer(Transform player)
    {
        // direction towards the goal (towards player)
        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 position = transform.position;

        // move ai towards the direction set at chase speed
        position += (direction * chaseSpeed * Time.deltaTime);
        transform.position = position;
    }

    /*
    void Patrol(GameObject patrolGoal)
    {
        float distance = Vector2.Distance(transform.position) patrolGoal.transform.position);

        if (distance > 0.05f)
        {
            
        }
    }
    */
}
