using UnityEngine;

public class EnemyStateMachine
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public EnemyState currentState { get; private set; }
    public void Initialize(EnemyState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
