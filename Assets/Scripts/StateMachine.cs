using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    #region Variables
    // enums declared outside of class can be refenced from other scripts
    public enum State
    {
        Patrol,
        Chase,
        Investigate,
    }

    public State currentState;
    public AIMovement aiMovement;

    #endregion
    private void Start()
    {
        aiMovement = GetComponent<AIMovement>();                // grabs the AiMovement script so we can use its functions in our states

        NextState();
    }

    private void NextState()
    {
        //runs one of the cases that matches the value (in this example the value is currentState)
        switch (currentState)
        {
            case State.Chase:
                StartCoroutine(ChaseState());
                break;
            case State.Investigate:
                StartCoroutine(InvestigateState());
                break;
            case State.Patrol:
                StartCoroutine(PatrolState());
                break;
        }
    }

    #region State Coroutines
    //Coroutine is a special method that can be paused and returned to later
    private IEnumerator ChaseState()
    {
        Debug.Log("Chase: Enter");


        while (currentState == State.Chase)
        {
            aiMovement.AIMoveTowards(aiMovement.player);

            if (Vector2.Distance(transform.position, aiMovement.player.position)
                >= aiMovement.chaseDistance)
            {
                currentState = State.Investigate;
            }

            yield return null;
        }
        Debug.Log("Chase: Exit");
        NextState();
    }

    private IEnumerator InvestigateState()
    {
        Debug.Log("Investigate: Enter");
        while (currentState == State.Investigate)                                          // while in the investigation state...
        {
            Debug.Log("Currently Investigating");   
            yield return new WaitForSeconds(3);                                            // wait for 3 seconds
            currentState = State.Patrol;                                                   // then return to patrol
        }
   
        Debug.Log("Investigate: Exit");
        NextState();
    }



    private IEnumerator PatrolState()
    {
        Debug.Log("Patrol: Enter");

        while (currentState == State.Patrol)                                                // while in the patrol state...
        {
            //update

            aiMovement.AIMoveTowards(aiMovement.patrolGoals[aiMovement.goalIndex]);         // move towards the patrol goal in the array
            aiMovement.WaypointUpdate();                                                    // update the patrol goals

            if (Vector2.Distance(transform.position, aiMovement.player.position)            // if the player comes within chase distance
                                    < aiMovement.chaseDistance)
            {
                currentState = State.Chase;                                                 // change state to chase
            }

            yield return null;
        }
        Debug.Log("Patrol: Exit");
        NextState();
    }
#endregion
}
