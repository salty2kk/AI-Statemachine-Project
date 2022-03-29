using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // enums declared outside of class can be refenced from other scripts
    public enum State
    {
        Patrol,
        Chase,
        Investigate,
    }

    public State currentState;

    public AIMovement aiMovement;


    private void Start()
    {
        aiMovement = GetComponent<AIMovement>();

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
        while (currentState == State.Investigate)
        {
            Debug.Log("Currently Investigating");
            yield return new WaitForSeconds(3);
            currentState = State.Patrol;
        }
        yield return null;
        Debug.Log("Investigate: Exit");
        NextState();
    }



    private IEnumerator PatrolState()
    {
        Debug.Log("Patrol: Enter");

        while (currentState == State.Patrol)
        {
            //update

            aiMovement.AIMoveTowards(aiMovement.patrolGoals[aiMovement.goalIndex]);

            if (Vector2.Distance(transform.position, aiMovement.player.position)
                                    < aiMovement.chaseDistance)
            {
                currentState = State.Chase;
            }

            yield return null;
        }
        Debug.Log("BerryPicking: Exit");
        NextState();
    }
}
