using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState CurrentState { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize(PlayerState _startState)
    {
        CurrentState = _startState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState _newState)
    {
        CurrentState.Exit();
        CurrentState = _newState;
        CurrentState.Enter();
    }



}
