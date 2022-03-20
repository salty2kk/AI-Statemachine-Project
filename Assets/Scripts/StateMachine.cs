using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // enum is declared outside of class so it can be refenced from other scripts
    public enum State
    {
        Patrol,
        Chase,
        Investigate,
    }

    public State currentState;

}
