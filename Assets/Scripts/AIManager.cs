using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class AIManager : BaseManager
{
    public enum State
    {
        FullHP,
        LowHP,
        Dead,
    }

    public State currentState;
    protected PlayerManager _playerManager;

    protected override void Start()
    {
        base.Start();

        _playerManager = GetComponent<PlayerManager>();
        if (_playerManager == null)
        {
            Debug.LogError("PlayerManager not found");
        }
    }

    public override void TakeTurn()
    {
        if (_health <= 0f)
        {
            currentState = State.Dead;
        }

        switch (currentState)
        {
            case State.FullHP:
                FullHPState();
                if (_health > 0)
                {
                    Debug.Log("I'm Full Health!");
                }

                break;
            case State.LowHP:
                LowHPState();
                if (_health > 0)
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

    IEnumerator EndTurn()
    {
        yield return new WaitForSecondsRealtime(2f);
        _playerManager.TakeTurn();
    }

    void LowHPState()
    {    

        if (_health > 60f)
        {
            currentState = State.FullHP;
        }
    }

    void DeadState()
    {
        Debug.Log("AI IS DEAD YOU WIN");
    }

    void FullHPState()
    {
        if (_health < 40f)
        {
            currentState = State.LowHP;
            LowHPState();
            return;
        }

    }
}
