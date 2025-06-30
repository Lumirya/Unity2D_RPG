using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected Enemy_Skeleton enemy;

    public SkeletonGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) 
        : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        // Additional logic for entering the grounded state
        // For example, reset any timers or flags specific to this state
    }

    public override void Exit()
    {
        base.Exit();
        // Additional logic for exiting the grounded state
    }

    public override void Update()
    {
        base.Update();
        // Logic for updating the grounded state
        // For example, check for player proximity or other conditions to transition to another state
        if (enemy.IsPlayerDetected())
        {
            // If player is detected, transition to battle state
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
