using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : BaseManager
{
    #region Variables
    public enum State
    {
        FullHP,
        LowHP,
        Dead,
    }

    public State currentState;
    protected PlayerManager _playerManager;

    #endregion
    protected override void Start()
    {
        base.Start();

        _playerManager = GetComponent<PlayerManager>();
        if (_playerManager == null)
        {
            Debug.LogError("PlayerManager not found");
        }

        NextState();
    }

    public void NextState()                                 // this statemachine does not work properly, it does not debug the any other state than FullHP and I don't know why
    {
        switch (currentState)
        {
            case State.FullHP:
                FullHPState();
                if (_health >= 80)
                {
                    Debug.Log("I'm Full Health!");
                }
                break;
            case State.LowHP:
                LowHPState();
                if (_health <= 40)
                {
                    Debug.Log("I'm nearly Dead!");
                }
                break;
            case State.Dead:
                DeadState();
                if (_health <= 0)
                {
                    Debug.Log("I am Dead!");
                }
                break;
        }

    }

    #region States
    private IEnumerator LowHPState()
    {    
        if (_health >= 40f)
        {
            currentState = State.FullHP;
            FullHPState();
        }
        else
        {
            currentState = State.LowHP;
        }

        yield return null;
        NextState();
    }

    void DeadState()
    {
        Debug.Log("AI IS DEAD YOU WIN");
    }

    private IEnumerator FullHPState()
    {
        if (_health < 40f)
        {
            currentState = State.LowHP;
            LowHPState();
        }
        else
        {
            currentState = State.FullHP;
        }

        yield return null;
        NextState();
    }
    #endregion
}
